using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameManager
{
	public static int 	difficulty = 3;

	public static void 		StartNewGame()
	{
		SceneManager.LoadScene(1);
	}
	
	public static void 		BackToMenu()
	{
		SceneManager.LoadScene(0);
	}

}
