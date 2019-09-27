using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
//    RectTransform     
	
	public void    Button_Exit()
	{
		// save any game data here	
		Application.Quit();
//		EditorApplication.isPlaying = false;
	}
	
	public void     Button_StartANewGame()
	{
		GameManager.StartNewGame();
	}

	public void 	Button_TestAds()
	{
		GameManager.TestAds();
	}
	
	
	
	
	
	
	
	[Tooltip("This is for text in Difficulty panel, which saying who is your opponent")]
	public Text 	curDiffText;
	
	public void 	RefreshTexts()
	{
		curDiffText.text = "Your Opponent is \n";
		switch (GameManager.difficulty)
		{
			case 0 :
				curDiffText.text += "Bobik Randomik";
				break;
			case 1 :
				curDiffText.text += "Taras Normas";
				break;
			case 2 :
				curDiffText.text += "Rubik Skladnitsky";
				break;
			case 3 :
				curDiffText.text += "Friend";
				break;
			default :
				curDiffText.text += "is drinking Tea";
				break;
		}
	}
	
	public void 	SetDifficulty(int num)
	{
		GameManager.difficulty = num;
		RefreshTexts();
	}
}
