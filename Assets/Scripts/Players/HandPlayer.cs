using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlayer : SimplePlayer
{
	public RectTransform[]     inputTiles;
	
	public override void MakeTurn()
	{
		base.MakeTurn();
		
		UnlockAvailableTiles();
	}
	
	
	
	private void     UnlockAvailableTiles()
	{
		int i = 0;
		while (i < inputTiles.Length && i < analyzeTable.Length)
		{
			if (analyzeTable[i] == 0)
				inputTiles[i].gameObject.SetActive(true);
			i++;
		}
	}
	
	public void 	LockAllTiles()
	{
		int i = 0;
		while (i < inputTiles.Length)
		{
			inputTiles[i].gameObject.SetActive(false);
			i++;
		}
	}
}


