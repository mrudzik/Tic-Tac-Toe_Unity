using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class MediumPlayer : EasyPlayer
{
	
	protected override IEnumerator     AIBrains()
	{
		// Illusion of thinking
		yield return new WaitForSeconds(Random.Range(1f, 3f));
		
		// Special tactics
		AITactics();
	}
	
	protected virtual void		AITactics()
	{
		// This one will analyze important combos
		if (ComboAnalyzer())
			return;
		
		RandomTurn();
	}
	
	protected bool 		ComboAnalyzer()
	{
		// Find Win Combos for yourself
		if (FindCombos(playerNum))
			return true;
		// If none of win combos found
		// Try to find enemy win combo
		if (playerNum == 1)
		{
			if (FindCombos(2))
				return true;
		}
		else
		{
			if (FindCombos(1))
				return true;
		}
		// If there is nothing special to block
		// Continue to next phase
		return false;
	}
	
	
	
	private bool		FindCombos(int num)
	{
		// All Horizontal Checks
		for (int i = 0; i < 3; i++)
		{
			int line = 3 * i;
			if (CheckCombination(line, line + 1, line + 2))
				return true;
		}
		// All Vertical Checks
		for (int i = 0; i < 3; i++)
		{
			int line = i;
			if (CheckCombination(line, line + 3, line + 6))
				return true;
		}
		
		// All Diagonal Checks
		//	|1		|  1
		//	| 1		| 1	
		//	|  1	|1
		
		if (CheckCombination(0, 4, 8))
			return true;	
		if (CheckCombination(2, 4, 6))
			return true;
		
		// End of Method now
		return false;
		
		
		
		
		// Utility Function
		bool CheckCombination(int t1, int t2, int t3)
		{
			// 0 1 1
			if (	analyzeTable[t1] == 0
			&& 	analyzeTable[t2] == num
			&& 	analyzeTable[t3] == num)
			{
				EndTurn(t1);
				return true;
			}
			// 1 0 1
			if (	analyzeTable[t1] == num
			&& 	analyzeTable[t2] == 0
			&& 	analyzeTable[t3] == num)
			{
				EndTurn(t2);
				return true;
			}
			// 1 1 0
			if (	analyzeTable[t1] == num
			&& 	analyzeTable[t2] == num
			&& 	analyzeTable[t3] == 0)
			{
				EndTurn(t3);
				return true;
			}
			return false;
		}
	}
	
	
	
	
	

}
