using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayer : MonoBehaviour
{
	public string	Name { get; set; }
	[HideInInspector]
	public int 		playerNum;
	// 1 - X
	// 2 - O
	
	[HideInInspector] public PlayControl ourPlay;
	protected int[] analyzeTable;
	
	public virtual void 	MakeTurn()
	{
		analyzeTable = ourPlay.GetTable();
		
	}
	
	public void 	EndTurn(int num)
	{
		if (!SelectTile(num))
			throw new Exception("Wrong selected Tile");
		
		
		ourPlay.SetNewTable(analyzeTable);
		ourPlay.crossTurn = !ourPlay.crossTurn;
		ourPlay.NextTurn();
	}

	
	public bool 	SelectTile(int tileNum)
	{
		if (tileNum < 0 && tileNum > 8)
			return false;
		
		analyzeTable[tileNum] = playerNum;
		return true;
	}
	
	
}
