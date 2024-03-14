using GoogleMobileAds.Api.Mediation;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

using System;
using System.Collections;
using UnityEngine;


public class AdMob : MonoBehaviour
{
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-5205187543072249/3674585737";
    ////  ca-app-pub-5205187543072249/3674585737
    public string bannerAdID
    {
#if UNITY_EDITOR
        get { return bannerTestID; }
#else
        get { return bannerID; }
#endif
    }

    BannerView bannerView;
    AdSize adaptiveSize;


    private RewardedAd rewardedAd;
    private const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    private const string rewardID = "ca-app-pub-5205187543072249/1819379014";

    public string rewardAdId
    {
#if UNITY_EDITOR
        get { return rewardTestID; }
#else
        get { return rewardID; }
#endif
    }

    private void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
            .SetMaxAdContentRating(MaxAdContentRating.G)
            .build();

        //RequestConfiguration requestConfiguration = new RequestConfiguration()
        //{
        //    TagForChildDirectedTreatment = TagForChildDirectedTreatment.True,
        //    MaxAdContentRating = MaxAdContentRating.G
        //};

        MobileAds.SetRequestConfiguration(requestConfiguration);

        //// Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((init) =>
        {
            CreateBannerView();
            LoadRewardedAd();
        });
    }

    private AdRequest GetRequest() => new AdRequest.Builder().Build();


    public void CreateBannerView()
    {
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }


        adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        bannerView = new BannerView(bannerAdID, adaptiveSize, AdPosition.Bottom);
        bannerView.LoadAd(GetRequest());

        bannerView.OnAdLoaded += BannerView_OnBannerAdLoaded;
    }

    private void BannerView_OnBannerAdLoaded(object sender, EventArgs e)
    {
        ComponentManager.instance.admobHeight = adaptiveSize.Height;
    }

    private void DestroyBannerView()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }

    private void OnDestroy()
    {
        DestroyBannerView();
        DestroyRewordAd();
    }




    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        Debug.Log("Loading the rewarded ad.");
        rewardedAd = new RewardedAd(rewardAdId);
        rewardedAd.LoadAd(GetRequest());
        rewardedAd.OnUserEarnedReward += RewardedAd_OnAdFullScreenContentClosed;
        //rewardedAd.LoadAd(rewardAdId, GetRequest(), (ad, error) =>
        //{
        //    if (error != null || ad == null)
        //    {
        //        Debug.LogError("Rewarded ad failed to load an ad " +
        //                       "with error : " + error);
        //        return;
        //    }

        //    Debug.Log("Rewarded ad loaded with response : "
        //              + ad.GetResponseInfo());
        //    rewardedAd = ad;


        //});


    }



    private void RewardedAd_OnAdFullScreenContentClosed(object sender, EventArgs e)
    {
        Debug.Log("Rewarded Ad full screen content closed.");
        LoadRewardedAd();
    }

    private void DestroyRewordAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }


    private IEnumerator ShowUser()
    {
        yield return null;
        //const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            LoadRewardedAd();
            //((ad) =>
            //{
            // TODO: Reward the user.
            //     Debug.Log(String.Format(rewardMsg, ad.Type, ad.Amount));
            // });
        }

    }

    public void UserChoseToWatchAd()
    {
        StartCoroutine(ShowUser());
    }
}
