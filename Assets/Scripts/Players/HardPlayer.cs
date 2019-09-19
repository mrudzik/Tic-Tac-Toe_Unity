using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPlayer : MediumPlayer
{
	protected override void AITactics()
	{
		// Needed for planing
		turnCount++;
		
		// This one will analyze important combos
		if (ComboAnalyzer())
			return;
		// This one will try to do some strategy
		if (AIPlanner())
			return;
		
		// If all above fails just use Random
		RandomTurn();
	}

	private int turnCount = -1;
	private int xTactic = -1;
	private int oTactic = -1;
	private bool     AIPlanner()
	{
		
		if (playerNum == 1)
		{
			if (XStrategy())
				return true; 
		}
		else
		{
			if (OStrategy())
				return true;
		}
		
		
		return false;
	}





	private bool     XStrategy()
	{
		if (turnCount == 0)
			xTactic = Random.Range(0, 2);
		
		if (xTactic == 0)
		{// This is an central tactic
			if (XCentralTactics())
				return true;	
		}
		else
		{// This is an corner fork tactic
			if (XCornerTactics())
				return true;
		}
			

		return false;
	}
	
	private bool 	XCentralTactics()
	{
		if (turnCount == 0)
		{// This will place cross on center
			EndTurn(4);	// - - -
			return true;		// - X -
		}						// - - -
		if (turnCount == 1)
		{
			if (analyzeTable[1] == 2)
			{						// - O X
				oTactic = 1;		// - X -
				EndTurn(2);	// - - -
				return true;
			}
			if (analyzeTable[5] == 2)
			{						// - - -
				oTactic = 5;		// - X O
				EndTurn(8);	// - - X
				return true;
			}
			if (analyzeTable[7] == 2)
			{						// - - -
				oTactic = 7;		// - X -
				EndTurn(6);	// X O -
				return true;
			}
			if (analyzeTable[3] == 2)
			{						// - - -
				oTactic = 3;		// O X -
				EndTurn(6);	// X - -
				return true;
			}
			// If we are here
			// This tactic failed
			xTactic = -1;
		}
		if (turnCount == 2)
		{
			switch (oTactic)
			{
				case 1:					// - 0 X
					EndTurn(8);	// - X -
					return true;		// 0 - X
					
				case 5:					// 0 - -
					EndTurn(6);	// - X 0
					return true;		// X - X
					
				case 7:					// X - -
					EndTurn(0);	// - X -
					return true;		// X O -
					
				case 3:					// - - -
					EndTurn(8);	// O X -
					return true;		// X - X
			}
			// The all tactic ends right here
			xTactic = -1;
		}
		return false;
	}

	private int 	xCorner = -1;
	private bool 	XCornerTactics()
	{
		if (turnCount == 0)
		{
			int rand = Random.Range(0, 4);
			switch (rand)
			{
				case 0:					// X - -
					xCorner = 0;		// - - -
					EndTurn(xCorner);	// - - -
					return true;
				case 1:					// - - X
					xCorner = 2;		// - - -
					EndTurn(xCorner);	// - - -
					return true;
				case 2:
					xCorner = 8;		// - - -
					EndTurn(xCorner);	// - - - 
					return true;		// - - X
				case 3:
					xCorner = 6;		// - - -
					EndTurn(xCorner);	// - - -
					return true;		// X - -
			}
		}
		if (turnCount == 1)
		{
			if (xCorner == 0 && analyzeTable[8] == 2	// Example
			|| xCorner == 2 && analyzeTable[6] == 2		// X - -
			|| xCorner == 8 && analyzeTable[0] == 2 	// - - -
			|| xCorner == 6 && analyzeTable[2] == 2)	// - - O
			{	// O Position is on opposite corner
				// Such positions will ruin our tactic
				xTactic = -1;
				return false;
			}
			
			
			// The possibility of opposite corner is now off
			if (analyzeTable[4] == 2 	// Checking
			|| analyzeTable[0] == 2		// * - *
			|| analyzeTable[2] == 2 	// - * -
			|| analyzeTable[6] == 2 	// * - *
			|| analyzeTable[8] == 2)
			{// If O in center or on non-opposite corner
				switch (xCorner)
				{
					case 0 :				// X - *
						EndTurn(8); 	// - * -
						return true;		// * - X
					
					case 2 :				// * - X
						EndTurn(6);	// - * -
						return true;		// X - *
					
					case 8 :				// X - *
						EndTurn(0);	// - * -
						return true;		// * - X
					
					case 6 :				// * - X
						EndTurn(2);	// - * -
						return true;		// X - *
				}
				// This should guaranty the WIN
			}
			
			
			// Side Logic
			if (analyzeTable[1] == 2	// Checking
			|| analyzeTable[5] == 2		// - * -
			|| analyzeTable[7] == 2		// * - *
			|| analyzeTable[3] == 2)	// - * -
			{// If 0 is placed somewhere at side	// Example
				// We just place X on center		// X 0 -
				EndTurn(4);					// - X -
				return true;						// - - -
			}
			
		}
		
		if (turnCount == 2)
		{
			// The possibility of 0 on center and somewhere on side is off
			// Because ComboAnalyzer Handles it
			
			// If O in center or on non-opposite corner
			// possibility is off its on ComboAnalyzer
			
			
			// Side Logic
			if (analyzeTable[1] == 2	// Checking
			|| analyzeTable[5] == 2		// - * -
			|| analyzeTable[7] == 2		// * - *
			|| analyzeTable[3] == 2)	// - * -
			{	// Now we have situation 	// Example
				switch (xCorner)			// X 0 -
				{// Must place Fork			// - X -
											// - - 0
					
					case 0 :
						if (analyzeTable[1] == 2) // case 1
						{						// X  *1 f2 - fork potential position
							EndTurn(6);	// *2 X  c - handled by Combo Analyzer
							return true;		// f1 c  0
						}// case 2
						EndTurn(2); 	
						return true;		
					
					case 2 :
						if (analyzeTable[1] == 2) // case 1
						{
							EndTurn(8);
							return true;
						}// case 2 			// f2 *1 X
						EndTurn(0);	// c  X  *2
						return true;		// 0  c  f1
					
					case 8 :
						if (analyzeTable[5] == 2) // case 1
						{
							EndTurn(6);
							return true;
						}// case 2 			// 0  c  f2
						EndTurn(2);	// c  X  *1
						return true;		// f1 *2 X
					
					case 6 :
						if (analyzeTable[3] == 2)
						{
							EndTurn(8);
							return true;
						}// case 2			// f2 c  0 
						EndTurn(0);	// *1 X  c
						return true;		// X  *2 f1
				}
				
			}
		}
			

		return false;
	}
	
	
	
	
	
	
	
	
	private bool     OStrategy()
	{
		if (turnCount == 0)
		{
			// Check center for X
			if (analyzeTable[4] == 1)
			{ // X is on center
				// Block possibility for fork
				// After this turn there is no specific tactics
				// Combo Analyzer should handle it
				oTactic = -1;					// o - o
				int rand = Random.Range(0, 4);	// - X -
				switch (rand)					// o - o
				{
					case 0 :
						EndTurn(0);
						return true;
					case 1 :
						EndTurn(2);
						return true;
					case 2 :
						EndTurn(8);
						return true;
					case 3 :
						EndTurn(6);
						return true;
				}
			}
			else
			{ // X is not on center
				if (analyzeTable[0] == 1	// Example
				|| analyzeTable[2] == 1		// X - -
				|| analyzeTable[8] == 1		// 0 0 -
				|| analyzeTable[6] == 1)	// - - -
				{	// Go to Offence by placing on middle
					oTactic = 1;
				}
				EndTurn(4);
				return true;
			}
		}
		if (turnCount == 1)
		{
			if (oTactic == 1)
			{ // Going offence
				// No matter where to place
				// matters only that it must have a possibility to
				// achieve win if enemy is sleeping
				if (analyzeTable[1] == 0 && analyzeTable[7] == 0)
				{
					EndTurn(1);
					return true;
				}
				if (analyzeTable[5] == 0 && analyzeTable[3] == 0)
				{
					EndTurn(3);
					return true;
				}
			}
		}
		if (turnCount == 2)
		{
			if (oTactic == 1)
			{	// At this moment we need to check corners
				// Cuz we can be lose initiative and fail the game
				if (analyzeTable[0] == 0		// o x -
				    && analyzeTable[1] == 1		// x 0 -
				    && analyzeTable[3] == 1)	// - - -
				{
					EndTurn(0);
					return true;
				}
				if (analyzeTable[2] == 0		// - x o
				    && analyzeTable[1] == 1		// - 0 x
				    && analyzeTable[5] == 1)	// - - -
				{
					EndTurn(2);
					return true;
				}
				if (analyzeTable[8] == 0		// - - -
				    && analyzeTable[5] == 1		// - 0 x
				    && analyzeTable[7] == 1)	// - x o
				{
					EndTurn(8);
					return true;
				}
				if (analyzeTable[6] == 0		// - - -
				    && analyzeTable[3] == 1		// x 0 -
				    && analyzeTable[7] == 1)	// o x -
				{
					EndTurn(6);
					return true;
				}
				
			}
		}
		
		return false;
	}
	

}
