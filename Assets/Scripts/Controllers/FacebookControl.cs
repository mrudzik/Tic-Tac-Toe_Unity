using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;


public class FacebookControl : MonoBehaviour
{
	// Display friends who play this game too
	public Text 	FriendsText;
	private void Awake()
	{
		// Initialization part
		
		if (!FB.IsInitialized)
		{
			FB.Init(() => // Using lambda
			{
				if (FB.IsInitialized)
					FB.ActivateApp();
				else
					Debug.LogError("Could not initialize FB");
			},
			isGameShown =>
			{
				if (!isGameShown)
					Time.timeScale = 0;
				else
					Time.timeScale = 1;
			});
		}
		else
			FB.ActivateApp();
	}


	#region Login / Logout
	
	public void 	FacebookLogin()
	{
		var permissions = new List<string>() {"public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(permissions);
	}
	
	public void 	FacebookLogout()
	{
		FB.LogOut();
	}
	
	#endregion
	
	
	
	public void 	FacebookShare()
	{
		FB.ShareLink(new System.Uri("http://youtube.com"),
			"Check this out",
			"Testing my facebook sharing scripts, lol",
			new System.Uri("https://i.kym-cdn.com/photos/images/original/000/943/764/358.jpg"));
	}
	
	
	
	
	#region Inviting
	
	public void 	FacebookGameRequest()
	{
		FB.AppRequest("Hey! Im testing App Request", title: "Tic-Tac-Toe Testing");
	}
	
	public void 	FacebookInvite()
	{
//		FB.Mobile.AppInvite(new System.Uri("link to playstore here"));
		// TODO invite to play store
		Debug.Log("Must be invite to PlayStore here");
	}
	
	#endregion
	
	
	public void 	GetFriendsPlayingThisGame()
	{
		string query = "me/friends";
		FB.API(query, HttpMethod.GET, result =>
		{
			var dictionary = (Dictionary<string, object>)
				Facebook.MiniJSON.Json.Deserialize(result.RawResult);
			var friendsList = (List<object>) dictionary["data"];
			FriendsText.text = "Friends who play:\n";

			foreach (var dict in friendsList)
			{
				FriendsText.text += ((Dictionary<string, object>)dict)["name"];
			}
		});

	}
	
	
	
}
