using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdControl : MonoBehaviour
{
	[SerializeField] private string adAppID = "ca-app-pub-9930314891066660~4370848487";

 // Production ID's
	[SerializeField] private string adBannerID = "ca-app-pub-9930314891066660/6744702717";
	[SerializeField] private string adRegularID = "ca-app-pub-9930314891066660/1272884183";
	[SerializeField] private string adRewardID = "ca-app-pub-9930314891066660/1133283384";

	[SerializeField] private string deviceID = "D7B04A2464E4B20";

////// Test ID's
//
//	[SerializeField] private string adBannerID = "ca-app-pub-3940256099942544/6300978111";
//	[SerializeField] private string adRegularID = "ca-app-pub-3940256099942544/1033173712";
//	[SerializeField] private string adRewardID = "ca-app-pub-3940256099942544/5224354917";

	private void Awake()
	{
		// Initializing our app ID 
		MobileAds.Initialize(adAppID);
	}

	private BannerView    bannerView;
	private InterstitialAd adRegular;
	private RewardBasedVideoAd rewardAd;
	
	
	
	public void RefreshBanner()
	{
//		// Creating empty banner
		bannerView?.Destroy(); // Leak protection
		bannerView = new BannerView(adBannerID, AdSize.Banner, AdPosition.Bottom);
		
		// Asking which ad to show, and getting one
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice(deviceID) // Remove on Release
			.Build();
		
		// Loading proper add to our banner
		bannerView.LoadAd(request);
		bannerView.Show();
	}


	public Text debugText;
	
	public void RefreshRegular()
	{
		adRegular?.Destroy(); // Leak protection
		adRegular = new InterstitialAd(adRegularID);
		
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice(deviceID) // Remove on Release
			.Build();
		adRegular.LoadAd(request);
		if (adRegular.IsLoaded())
		{
			adRegular.Show();
			debugText.text = "Loaded";
			
		}
		else
		{
			debugText.text = "Regular Not loaded";
		}

	}
	
	public void RefreshReward()
	{
		rewardAd = RewardBasedVideoAd.Instance;
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice(deviceID) // Remove on Release
			.Build();
		rewardAd.LoadAd(request, adRewardID);

		
		if (rewardAd.IsLoaded())
		{
			rewardAd.Show();
			debugText.text = "Loaded";
		}
		else
        {
        	debugText.text = "Reward Not loaded";
        }
	}
	
}

