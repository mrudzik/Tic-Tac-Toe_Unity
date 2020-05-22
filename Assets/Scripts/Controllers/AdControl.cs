using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdControl : MonoBehaviour
{
	[SerializeField] private string adAppID = "";

 // Production ID's
	[SerializeField] private string adBannerID = "";
	[SerializeField] private string adRegularID = "";
	[SerializeField] private string adRewardID = "";

	[SerializeField] private string deviceID = "D7B04A2464E4B20";

////// Test ID's
//
//	[SerializeField] private string adBannerID = "";
//	[SerializeField] private string adRegularID = "";
//	[SerializeField] private string adRewardID = "";

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

