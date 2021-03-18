using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Common;
using UnityEngine.Events;

public class Rewarded : MonoBehaviour
{
    RewardedAd rewardAd;
    public string iphoneID;
    public string androidID;
    public Animator[] animators;
    public AudioSource music;
    public Button watchAd;
    public UnityEvent RewardedFunction;
    string rewardId;
    void Start()
    { 
        RequestRewardedAd();
    }
   

    //make button interactable if video ad is ready
    /*void Update()
    {
        if (rewardAd.IsLoaded())
        {
            watchAd.interactable = true;
        }
    }*/

    void RequestRewardedAd()
    {
#if UNITY_ANDROID
        rewardId = androidID;
#elif UNITY_IPHONE
        rewardId = iphoneID;
#else
        rewardId = null;
#endif
        rewardAd = new RewardedAd(rewardId);

        //call events
        rewardAd.OnAdLoaded += HandleRewardAdLoaded;
        rewardAd.OnAdFailedToLoad += HandleRewardAdFailedToLoad;
        rewardAd.OnAdOpening += HandleRewardAdOpening;
        rewardAd.OnAdFailedToShow += HandleRewardAdFailedToShow;
        rewardAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardAd.OnAdClosed += HandleRewardAdClosed;


        //create and ad request
        if (PlayerPrefs.HasKey("Consent"))
        {
            AdRequest request = new AdRequest.Builder().Build();
            rewardAd.LoadAd(request); //load & show the banner ad
        }
        else
        {
            AdRequest request = new AdRequest.Builder().AddExtra("npa", "1").Build();
            rewardAd.LoadAd(request); //load & show the banner ad (non-personalised)
        }
    }

    //attach to a button that plays ad if ready
    public void ShowRewardedAd()
    {
        if (rewardAd.IsLoaded())
        {
            rewardAd.Show();
        }
    }

    //call events
    public void HandleRewardAdLoaded(object sender, EventArgs args)
    {
        //do this when ad loads
      
    }

    public void HandleRewardAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        //do this when ad fails to loads
        Debug.Log("Ad failed to load" + args.Message);
    }

    public void HandleRewardAdOpening(object sender, EventArgs args)
    {
        //do this when ad is opening
        Time.timeScale = 0;
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].enabled = false;
        }
        music.volume = 0;
    }

    public void HandleRewardAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        //do this when ad fails to show
        
    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        //reward the player here
        //RevivePlayer();
        RewardedFunction.Invoke();
        
    }

    public void HandleRewardAdClosed(object sender, EventArgs args)
    {
        //do this when ad is closed
        FindObjectOfType<AudioManager>().soundUpgrade();
        Time.timeScale = 1;
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].enabled = true;
        }
        Debug.Log("continue playing!!");
        music.volume = .6f;
        RequestRewardedAd();
    }

}
