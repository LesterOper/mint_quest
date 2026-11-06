using System;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

namespace Library.Common
{
    public class AdsHelper : MonoBehaviour
    {
        [SerializeField] private string _androidIronSourceId;
        [SerializeField] private string _iosIronSourceId;

        private VideoShownHandler _onVideoShown;
        private VideoAdsPlace _videoPlace;
        private volatile ThreadHelper _threadHelper;
        private bool _initialied = false;
        private float bannerHeight = 0;

        public float BannerHeight => bannerHeight;
        public bool INITIALIZED => _initialied;

        public bool isBannerShown;

        public event Action<bool> OnVideoStateChanged;

        private void Start()
        {

            if (_androidIronSourceId == "")
            {
                return;
            }
            
            Debug.Log("[AdsHelper] Start : ");

            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            IronSourceEvents.onBannerAdClickedEvent += BannerClickedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;

#if UNITY_ANDROID
            IronSource.Agent.init(_androidIronSourceId, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
            //IronSource.Agent.init(_androidIronSourceId, IronSourceAdUnits.INTERSTITIAL);
            //IronSource.Agent.init(_androidIronSourceId, IronSourceAdUnits.BANNER);
#elif UNITY_IOS
            IronSource.Agent.init (_iosIronSourceId, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
#endif

            
            _initialied = true;
            IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
            IronSource.Agent.loadInterstitial();
            UpdateBannerVisibility();
            IronSource.Agent.validateIntegration();
            IronSource.Agent.setAdaptersDebug(true);
        }

        public bool HasVideo()
        {
#if UNITY_EDITOR
            return true;
#else
            return IronSource.Agent.isRewardedVideoAvailable();
#endif
        }

        public void PlayVideo(VideoShownHandler callback, VideoAdsPlace place)
        {
#if UNITY_EDITOR
            callback.Invoke();
#else
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                if (_threadHelper == null)
                    _threadHelper = GetComponent<ThreadHelper>();

                IronSource.Agent.showRewardedVideo();
                _videoPlace = place;
                _onVideoShown = callback;
            }
#endif
        }

        public void ShowInterstitial()
        {
            // if (!PremPurchaseUtils.IsPurchased(PurchaseName.Premium)&& !PremPurchaseUtils.IsPurchased(PurchaseName.NoAd))
            // {
           
            // TODO Change to legal purchasing
            Debug.Log("INTERSTITIAL: " + Context.DataHelper.GetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED));
            if (Context.DataHelper.GetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED) == 0)
            {
                if (IronSource.Agent.isInterstitialReady())
                {
                    IronSource.Agent.showInterstitial();
                    IronSource.Agent.loadInterstitial();
                }
            }
            // }
        }

        public void DisableAds()
        {
            IronSource.Agent.hideBanner();
        }

        #region Rewarded
        private void RewardedVideoAdOpenedEvent()
        {
            Debug.Log("[AdsHelper] RewardedVideoAdOpenedEvent : ");
        }

        private void RewardedVideoAdClosedEvent()
        {
            Debug.Log("[AdsHelper] RewardedVideoAdClosedEvent : ");
        }

        private void RewardedVideoAvailabilityChangedEvent(bool b)
        {
            Debug.Log("[AdsHelper] RewardedVideoAvailabilityChangedEvent : " + b);
            if (_threadHelper == null)
                _threadHelper = GetComponent<ThreadHelper>();

            _threadHelper.Enqueue(() => OnVideoStateChanged?.Invoke(b));
        }

        private void RewardedVideoAdStartedEvent()
        {
            Debug.Log("[AdsHelper] RewardedVideoAdStartedEvent : ");
        }

        private void RewardedVideoAdEndedEvent()
        {
            Debug.Log("[AdsHelper] RewardedVideoAdEndedEvent : ");
        }

        private void RewardedVideoAdRewardedEvent(IronSourcePlacement ironSourcePlacement)
        {
            Debug.Log("[AdsHelper] RewardedVideoAdRewardedEvent : " + ironSourcePlacement);
            
            // AnalyticsUtils.SendEvent(EventType.REWARDED_SHOWN, _videoPlace.ToString());
            
            _threadHelper.Enqueue(() =>
            {
                _onVideoShown?.Invoke();
                _onVideoShown = null;
                _videoPlace = VideoAdsPlace.NONE;
            });
            
        }

        private void RewardedVideoAdShowFailedEvent(IronSourceError ironSourceError)
        {
            Debug.Log("[AdsHelper] RewardedVideoAdShowFailedEvent : " + ironSourceError);
        }

        private void RewardedVideoAdClickedEvent(IronSourcePlacement ironSourcePlacement)
        {
            // AnalyticsUtils.SendEvent(EventType.ADS_CLICKED, AdsType.Rewarded_.ToString());
        }

        #endregion
        #region Interstitial
        private void InterstitialAdReadyEvent()
        {
            Debug.Log("[AdsHelper] InterstitialAdReadyEvent : ");
        }
        private void InterstitialAdLoadFailedEvent(IronSourceError ironSourceError)
        {
            IronSource.Agent.loadInterstitial();
            Debug.Log("[AdsHelper] InterstitialAdLoadFailedEvent : " + ironSourceError);
        }
        private void InterstitialAdShowSucceededEvent()
        {
            Debug.Log("[AdsHelper] InterstitialAdShowSucceededEvent");
        }
        private void InterstitialAdShowFailedEvent(IronSourceError ironSourceError)
        {
            Debug.Log("[AdsHelper] InterstitialAdShowFailedEvent : " + ironSourceError);
        }
        private void InterstitialAdClickedEvent()
        {
            // AnalyticsUtils.SendEvent(EventType.ADS_CLICKED, AdsType.Interstitial.ToString());
            
            Debug.Log("[AdsHelper] InterstitialAdClickedEvent");
        }
        private void InterstitialAdOpenedEvent()
        {
            Debug.Log("[AdsHelper] InterstitialAdOpenedEvent");
        }
        private void InterstitialAdClosedEvent()
        {
            // AnalyticsUtils.SendEvent(EventType.ADS_SHOWN, AdsType.Interstitial.ToString());
            
            Debug.Log("[AdsHelper] InterstitialAdClosedEvent");
        }
        
        #endregion

        private void BannerClickedEvent()
        {
            // AnalyticsUtils.SendEvent(EventType.ADS_CLICKED, AdsType.Banner.ToString());
        }

        private void BannerAdLoadFailedEvent (IronSourceError error)
        {
            isBannerShown = false;
            Debug.Log("ADS HELPER: BANNER FAILED " + error);
        }

        private void BannerAdLoadedEvent()
        {
            isBannerShown = true;
            bannerHeight = IronSourceBannerSize.SMART.Height;
            //IronSource.Agent.displayBanner();
        }

        public void UpdateBannerVisibility()
        {
            Debug.Log($"[AdsHelper] UpdateBannerVisibility : initialized =  {_initialied}");
            Debug.Log("Banner: " + Context.DataHelper.GetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED));
            if (Context.DataHelper.GetLong(AppConstans.ADS_DISABLE_WAS_PURCHASED) == 0)
            {
                if (_initialied)
                {
                    isBannerShown = true;
                    Debug.Log("[AdsHelper] UpdateBannerVisibility : ");
                    IronSource.Agent.displayBanner();
                }
            }
        }

        public delegate void VideoShownHandler();
        
        public enum VideoAdsPlace
        {
            CoinByAd,
            BallByAd,
            OpenDialog,
            
            NONE,
        }
    }
}