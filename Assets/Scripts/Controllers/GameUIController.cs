using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
	
	public Sprite 	XSprite;
	public Sprite 	OSprite;
	public Sprite 	NothingSprite;
	public Image[]		UIBoxes;	

	public void 	ShowTableOnUI(int[] inputTable)
	{
		// Tip for game table
		// 0 1 2
		// 3 4 5
		// 6 7 8
		
		if (inputTable.Length != 9)
			throw new Exception("Invalid table");

		int i = 0;
		while (i < inputTable.Length)
		{
			switch (inputTable[i])
			{
				case 1: // Means X
					UIBoxes[i].sprite = XSprite;
					break;
				case 2: // Means O
					UIBoxes[i].sprite = OSprite;
					break;
				default:
					UIBoxes[i].sprite = NothingSprite;
					break;
			}
			i++;
		}

	}



	// Turn Logic

	public Text 	sayTurn;
	public Image 	player1Panel;
	public Image 	player2Panel;
	public void 	RefreshTurnText(string name, int mark, bool isPlayer)
	{
		if (mark != 1 && mark != 2)
			return;
		// Middle Text Logic
		sayTurn.text = "Turn: " + name + "\nMark is ";
		if (mark == 1)
			sayTurn.text += "X   ";
		else
			sayTurn.text += "O   ";
		
		// Side Panels Logic
		
		// Cool Fading
		StartCoroutine(FadeImage(player1Panel, 0.5f, !isPlayer));
		StartCoroutine(FadeImage(player2Panel, 0.5f, isPlayer));


	}
	
	IEnumerator	FadeImage(Image source, float time, bool fadeOut)
	{
		float timer = 0;
		float smoothness = 0.01f;
		Color tmpColor = source.color;
		
		while (timer < time)
		{
			
			if (fadeOut) // Fade Out
				tmpColor.a = 1.0f - Mathf.Clamp01(timer / time);
			else // Fade In
				tmpColor.a = Mathf.Clamp01(timer / time);
			// This will change color of source
			source.color = tmpColor;
			
			yield return new WaitForSeconds(smoothness);
			timer += smoothness;
		}

	}



	public Text 	name1;
	public Text 	name2;
	
	public Image 	leftImage;
	public Image 	rightImage;
	
	
	
	public void 	SetPlayerPics(SimplePlayer player1, SimplePlayer player2)
	{
		if (player1.playerNum == 1)
		{
			leftImage.sprite = XSprite;
			rightImage.sprite = OSprite;
		}
		else
		{
			leftImage.sprite = OSprite;
			rightImage.sprite = XSprite;
		}

		name1.text = player1.Name;
		name2.text = player2.Name;

	}
	
	
	
	
	
	
	
	
	
	
	
	// Score Logic

	public Text 	sayScore;
	public void 	RefreshScoreText(string player1, string player2,
		int wins, int ties, int loses)
	{
		sayScore.text = player1 + " | " + wins
		                + " - " + ties + " - " + loses + " | " + player2;
	}

	
	
	
	
	
	

	
	
	
	
	
	
	
	


	public Image[] 	winLines;
	public Text 	winText;
	public RectTransform winPanel;
	public void 	DrawWinLine(int direction) => 
		winLines[direction].gameObject.SetActive(true);
	
	public void 	ShowRoundWinner(string name)
	{
		winPanel.gameObject.SetActive(true);
		winText.text = "Round Complete\n" + "The winner is " + name;
	}
	
	
	public void 	HideAllWinLines()
	{
		winPanel.gameObject.SetActive(false);

		foreach (var line in winLines)
		{
			line.gameObject.SetActive(false);
		}
	}
	
	
	
	
	
	
	
	
	// Buttons
	
	public void     EndGame()
	{
		// Must save here all data
		
		// Then exit to menu
		GameManager.BackToMenu();
	}

	
	
	
	private void Start()
	{
		print("Hello this game is started brand new");
		HideAllWinLines();
	}
}
