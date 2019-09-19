using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EasyPlayer : SimplePlayer
{
	public override void MakeTurn()
	{
		base.MakeTurn();
		StartCoroutine(AIBrains());
	}
	
	
	
	
	
	protected virtual IEnumerator     AIBrains()
	{
		// Illusion of thinking
		yield return new WaitForSeconds(Random.Range(1f, 3f));
		
		RandomTurn();
	}
	
	protected void 	RandomTurn()
	{
		// This is a list 
		List<int> listOfIndexes = new List<int>();
		{
			int i = 0;
			while (i < analyzeTable.Length)
			{
				if (analyzeTable[i] == 0)
					listOfIndexes.Add(i);
				i++;
			}
		}
		if (listOfIndexes.Count == 0)
			throw new Exception("Gave to AI tied array");
		
		int resNum = Random.Range(0, listOfIndexes.Count);
		EndTurn(listOfIndexes[resNum]);
	}


}
