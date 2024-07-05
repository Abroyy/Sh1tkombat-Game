using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;
using System.Collections;
using System.Collections.Generic;


public class ReklamManager : MonoBehaviour
{
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-5426485864396954/5694175221";
#elif UNITY_IPHONE
    private string _adUnitId = "";
#else
    private string _adUnitId = " unused ";
#endif

    private RewardedAd _rewardedAd;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStaus) =>
        {

        });

        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        if(_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;

        }

        Debug.Log("Loading the rewarded ad.");

        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError(" Rewarded ad failed to load an ad " + " with error : " + error);
                    return;
                }
                Debug.Log(" Rewarded ad loaded with response : " + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
            
    }

    public void ShowRewardedAd()
    {
        if(_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("Reklam Gosterildi.");
            });
        }
    }
}

    
