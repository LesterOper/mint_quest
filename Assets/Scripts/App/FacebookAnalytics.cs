using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Facebook.Unity.Example;
using UnityEngine;

public class FacebookAnalytics : MonoBehaviour
{
    private void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            
            FB.Mobile.SetAdvertiserTrackingEnabled(true);
            FB.Mobile.SetAutoLogAppEventsEnabled(true);
            FB.Mobile.FetchDeferredAppLinkData((error) =>
            {
                if(string.IsNullOrEmpty(error.Url))
                    Debug.Log(error.Url);
            });
        }
        else
            FB.Init(FB.ActivateApp);   
        
        FB.Mobile.SetAdvertiserTrackingEnabled(true);
        FB.Mobile.SetAutoLogAppEventsEnabled(true);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                FB.Mobile.SetAdvertiserTrackingEnabled(true);
                FB.Mobile.SetAutoLogAppEventsEnabled(true);
                FB.Mobile.FetchDeferredAppLinkData((error) =>
                {
                    if (string.IsNullOrEmpty(error.Url))
                        Debug.Log(error.Url);
                });
            }
            else
                FB.Init(FB.ActivateApp);    
        }  
    }
}
