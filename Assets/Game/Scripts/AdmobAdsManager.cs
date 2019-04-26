using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

/// <summary>
/// Script that handle your banner , noraml and reward ads
/// </summary>

//this is for IAP ads
//check link for more details :- https://firebase.google.com/docs/admob/android/games#in-app_purchase_ads

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class AdmobAdsManager : MonoBehaviour
{

    public static AdmobAdsManager instance;

    public bool isTesting = true;

    [Header("For Banner")]
    public string Android_Banner_ID = "";
    public string IOS_Banner_ID = "";
    [Header("For Interstitial")]
    public string Android_Interstitial_ID = "";
    public string IOS_Interstitial_ID = "";
    [Header("For Reward")]
    public string Android_Reward_ID = "";
    public string IOS_Reward_ID = "";

    [Header("Dont Know ID try \"Device ID Finder for AdMob\" App")]
    public string Device_ID = "";

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    private float deltaTime = 0.0f;
    private static string outputMessage = "";

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        // Get singleton reward based video ad reference.
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        //here we request for all the ads we need
        RequestBanner();
        RequestInterstitial();
        RequestRewardBasedVideo();
    }

    void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    //.............................................................Methods used to request for ads
    //we use this methode to get the banner ads
    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = Android_Banner_ID;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            string adUnitId = IOS_Banner_ID;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load a banner ad.
        //replace createAdRequest with request when the games is submitting to store
        if (isTesting)
        {
            bannerView.LoadAd(createAdRequest());
        }
        else
        {
            bannerView.LoadAd(request);
        }
    }

    //we use this methode to get the Interstitial ads
    private void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = Android_Interstitial_ID;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            string adUnitId = IOS_Interstitial_ID;
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load an interstitial ad.
        //replace createAdRequest with request when the games is submitting to store
        if (isTesting)
        {
            interstitial.LoadAd(createAdRequest());
        }
        else
        {
            interstitial.LoadAd(request);
        }
    }

    // the following method is used when we are testing the ads
    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                //add you device ID below if ubable to find ID try "Device ID Finder for AdMob" app
                //link:- https://play.google.com/store/apps/details?id=pe.go_com.admobdeviceidfinder&hl=en
                .AddTestDevice(Device_ID)
                .Build();
    }

    //we use this methode to get the RewardBasedVideo ads
    private void RequestRewardBasedVideo()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = Android_Reward_ID;
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            string adUnitId = IOS_Reward_ID;
#else
            string adUnitId = "unexpected_platform";
#endif
        AdRequest request = new AdRequest.Builder().Build();
        //replace createAdRequest with request when the games is submitting to store
        if (isTesting)
        {
            rewardBasedVideo.LoadAd(createAdRequest(), adUnitId);
        }
        else
        {
            rewardBasedVideo.LoadAd(request, adUnitId);
        }
    }
    //.............................................................Methods used to request for ads

    //.............................................................Methods used to show for ads
    //use this methode to show ads
    public void ShowInterstitial()
    {

#if UNITY_EDITOR
        Debug.Log("Interstitial Working");
#elif UNITY_ANDROID
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            
            RequestInterstitial();
        }
#endif

    }

    //use this methode to show ads
    public void ShowRewardBasedVideo()
    {

#if UNITY_EDITOR
        Debug.Log("RewardVideo Working");
#elif UNITY_ANDROID
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            print("Reward based video ad is not ready yet.");
            //RequestRewardBasedVideo();
        }
#endif

    }

    //this methode is used to call the banner ads
    public void ShowBannerAds()
    {
        bannerView.Show();
    }

    //this methode is used to hide banner ads
    public void HideBannerAds()
    {
        bannerView.Hide();
    }

    //this methode is used to destroy banner ads
    public void DestroyBannerAds()
    {
        bannerView.Destroy();
    }

    //.............................................................Methods used to show for ads


    //call banc handlers are used to detect the status of ads
    //for example if you are providing reward to the player on seeing reward ads then you can check wheather the
    //player has seen the complete ad and then only provide him the reward

    //................................................................Interstitial callback handlers
#region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

#endregion
    //................................................................end of Interstitial callback handlers

    //................................................................RewardBasedVideo callback handlers
#region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoLoaded event received.");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoClosed event received");
    }

    //this below event is used to reward the player like gems , money etc
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " +
                type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoLeftApplication event received");
    }

#endregion
    //................................................................end of RewardBasedVideo callback handlers
}