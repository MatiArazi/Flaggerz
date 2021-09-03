﻿using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Rewarded : MonoBehaviour
{
    
    private RewardedAd rewardedAd;
    public string adUnitIdAndroid; // = "ca-app-pub-3940256099942544/5224354917";
    public string adUnitIdIOS; // = "ca-app-pub-3940256099942544/1712485313";
    public UnityEvent rewardFunction;

    // Start is called before the first frame update
    void Start()
    {
        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = adUnitIdAndroid;
#elif UNITY_IPHONE
            adUnitId = adUnitIdIOS;
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);


        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
        GameObject panel = GameObject.Find("ErrorAd");
        panel.transform.localScale = Vector3.one;
        panel.GetComponentInChildren<LeanAnimation>().Abrir();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
        Time.timeScale = 0;

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "+ args.Message);
        GameObject panel = GameObject.Find("ErrorAd");
        panel.transform.localScale = Vector3.one;
        panel.GetComponentInChildren<LeanAnimation>().Abrir();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        Time.timeScale = 1;
        rewardFunction.Invoke();
        
    }

    public void UserChoseToWatchAd()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
    
}
