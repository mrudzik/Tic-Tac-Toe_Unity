using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{
	private int []		playTable;
	private GameUIController	ourUI;


	public Transform 	playerHolder;
	
	public GameObject 	handPlayerPrefab;
	public GameObject 	easyPrefab;
	public GameObject 	mediumPrefab;
	public GameObject 	hardPrefab;
	
	
	private SimplePlayer player1;
	private SimplePlayer player2;
	
	private int wins; // wins of player 1
	private int ties;
	private int loses; // loses of player 1
	// Player 1  0 - 0 - 0 Player 2


	[HideInInspector] public bool 	crossTurn;
	public void 	NextTurn()
	{
		
		if (CheckGameEnd(1))
			return;
		if (CheckGameEnd(2))
			return;
		if (CheckTie())
			return;
		
		ourUI.RefreshScoreText(player1.Name, player2.Name, wins, ties, loses);

		if (crossTurn)
		{
			print("X Turn");
			if (player1.playerNum == 1)
			{
				ourUI.RefreshTurnText(player1.Name, 1, true);
				player1.MakeTurn();
			}
				
			else
			{
				ourUI.RefreshTurnText(player2.Name, 1, false);
				player2.MakeTurn();
			}
		}
		else
		{
			print("O Turn");
			if (player1.playerNum == 2)
			{
				ourUI.RefreshTurnText(player1.Name, 2, true);
				player1.MakeTurn();
			}	
			else
			{
				ourUI.RefreshTurnText(player2.Name, 2, false);
				player2.MakeTurn();
			}
		}
	}
	
	
	// Used by any players to receive current info
	public int[] 	GetTable()
	{
		return (int[])playTable.Clone();
	}
	
	// Used by any players to set their turn
	public void 	SetNewTable(int[] newTable)
	{
		// We can add protection from cheating here
		
		playTable = newTable;
		ourUI.ShowTableOnUI(playTable);
	}
	
	
	
	
	
	
	
	
	
	
	
	// End Game Stuff
	
	public bool 	CheckTie()
	{
		int count = 0;
		
		foreach (var num in playTable)
		{
			if (num == 0)
				count++;
		}

		if (count > 0)
			return false;
		
		GameWinBy(-1);
		return true;

	}
	
	public bool 	CheckGameEnd(int num)
	{
		bool isWin = false;
		
		// Upper Horizontal
		// Middle Horizontal
		// Low Horizontal
		CheckTiles(0, 1, 2, 0);
		CheckTiles(3, 4, 5, 1);
		CheckTiles(6, 7, 8, 2);
		
		// Left Vertical
		// Middle Vertical
		// Right Vertical
		CheckTiles(0, 3, 6, 3);
		CheckTiles(1,4,7, 4);
		CheckTiles(2,5,8, 5);
		
		// Diagonal Up Left -> Down Right
		// Diagonal Down Left -> Up Right
		CheckTiles(0,4,8, 6);
		CheckTiles(2, 4, 6, 7);
		
		void 	CheckTiles(int t1, int t2, int t3, int direction)
		{
			if (playTable[t1] == num && playTable[t2] == num && playTable[t3] == num)
			{
				ourUI.DrawWinLine(direction);
				GameWinBy(num);
				isWin = true;
			}
		}

		return isWin;
	}
	
	
	private void 	GameWinBy(int num)
	{
		if (num != -1 && num != 1 && num != 2)
			return;
		
		if (player1.playerNum == num)
		{
			ourUI.ShowRoundWinner(player1.Name);
			wins++;
		}
		else if (num == -1)
		{
			ourUI.ShowRoundWinner("Friendship :)");
			ties++;
		}
		else// cuz player 2 have matching num
		{
			ourUI.ShowRoundWinner(player2.Name);
			loses++;
		}
		
		// This will change turns corresponding the rules
		switch (num)
		{
			case -1: // Tie
				SwapTurns();
				break;
			case 1 : // Leaving all as it was
				
				break;
			case 2 : // Swapping
				SwapTurns();
				break;
		}

		// This will reload game in a few seconds
		StartCoroutine(ReloadGame(3f));
		// Just for more information 
		ourUI.RefreshScoreText(player1.Name, player2.Name, wins, ties, loses);

		
		// Function inside method
		// To keep away from repeatable code
		void SwapTurns()
		{
			if (player1.playerNum == 1)
			{
				player1.playerNum = 2;
				player2.playerNum = 1;
			}
			else
			{
				player1.playerNum = 1;
				player2.playerNum = 2;
			}
		}
	}

	
	
	
	
	
	
	
	
	
	
	
	
	
	// Game Setup Stuff

	IEnumerator 	ReloadGame(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		// I need this because players restarting
		int temp1 = player1.playerNum;
		int temp2 = player2.playerNum;
		
		
		ourUI.HideAllWinLines();
		InitGameTable();
		player1.playerNum = temp1;
		player2.playerNum = temp2;
		
	
		ourUI.SetPlayerPics(player1, player2);
		NextTurn();

	}
	
	
	private void 	InitGameTable()
	{
		playTable = new int[9];
		ourUI.ShowTableOnUI(playTable);
		
		crossTurn = true;
		
		// Initialize players
		
		// To not breed instances
		if (player1 != null)
			DestroyImmediate(player1.gameObject);
		if (player2 != null)
			DestroyImmediate(player2.gameObject);
		
		// Spawn fresh players
		player1 = Instantiate(handPlayerPrefab, playerHolder).GetComponent<SimplePlayer>();
		player1.Name = "Player Host";
		
		switch (GameManager.difficulty)
		{
			case 0:
				player2 = Instantiate(easyPrefab, playerHolder).GetComponent<SimplePlayer>();
				player2.Name = "Bobik Randomik";
				break;
			
			case 1:
				player2 = Instantiate(mediumPrefab, playerHolder).GetComponent<SimplePlayer>();
				player2.Name = "Taras Normas";
				break;
			
			case 2:
				player2 = Instantiate(hardPrefab, playerHolder).GetComponent<SimplePlayer>();
				player2.Name = "Rubik Skladnitsky";
				break;
			
			default:			
				player2 = Instantiate(handPlayerPrefab, playerHolder).GetComponent<SimplePlayer>();
				player2.Name = "Player Guest";
				break;
		}

		player1.ourPlay = this;
		player2.ourPlay = this;
	}





	

	private void Start()
	{
		// Find all needed components without dragging in inspector
		ourUI = GetComponent<GameUIController>();
		// ...
		
		// Initialize game table
		InitGameTable();
		// Prepare turn system
		player1.playerNum = 1; // X
		player2.playerNum = 2; // O
		
		
		ourUI.SetPlayerPics(player1, player2);
		NextTurn();
	}
	
	
	
}
