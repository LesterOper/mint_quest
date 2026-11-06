using System.Collections;
using System.Collections.Generic;
using App;
using UnityEngine;
using Facebook.Unity;

public static class FacebookAnalyticsHelper
{
    public static void SendEventLevelPassed(long level)
    {
        var dict = new Dictionary<string, object>
        {
            [AppEventParameterName.Level.ToString()] = level
        };

        SendEvent(AppEventName.AchievedLevel.ToString(), dict);
    }

    public static void SendEventLevelEachFivePassedLevels(long level)
    {
        var dict = new Dictionary<string, object>
        {
            ["Level achievement"] = level,
            ["Number of passed levels"] = (LevelUtils.GetLastOpenLevel()+1) + "/100",
        };
        
        SendEvent("Level each 5 passing event", dict);
    }

    public static void SendEventItemPurchased(string currency, string price)
    {
        var dict = new Dictionary<string, object>
        {
            [ParameterType.ContentType.ToString()] = "Disable ads",
            [ParameterType.Currency.ToString()] = currency.ToString(),
            [ParameterType.Price.ToString()] = price
        };

        SendEvent(EventTypeAnalytics.Purchased.ToString(), dict);           
    }

    public static void SendEventUsedClue(string clueType)
    {
        var dict = new Dictionary<string, object>(){[ParameterType.Clue.ToString()] = clueType}; 
            
        SendEvent(EventTypeAnalytics.UsedClue.ToString(), dict);
    }

    public static void SendAdsEvent(AdsAnalyticsEventType adsType, Dictionary<string, object> dict)
    {
        SendEvent(adsType.ToString(), dict);    
    }

    public static void SendAdsEvent(AdsAnalyticsEventType adsType, ParameterType parameterType, object parameter)
    {
        var dict = new Dictionary<string, object>(){ [parameterType.ToString()] = parameter};
        SendAdsEvent(adsType, dict);
    }
    private static void SendEvent(string type, Dictionary<string, object> objects)
    {
        Debug.Log($"[AnalyticsUtils] SendEvent() - {type} : {objects}");
        foreach (var obj in objects)
        {
            Debug.Log($"{obj.Key} - {obj.Value}");
        }
#if UNITY_EDITOR
#else
            FB.LogAppEvent
            (
                type,
                0,
                objects
                );
            AppMetrica.Instance.ReportEvent(type, objects);
//            Debug.Log($"[AnalyticsUtils] SendEvent() - {type} : {objects}");
#endif
    }
}