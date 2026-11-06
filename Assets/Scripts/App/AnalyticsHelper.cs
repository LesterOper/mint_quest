using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class AnalyticsHelper : MonoBehaviour
    {
        public static void SendEventLevelPassed(long level)
        {
            var dict = new Dictionary<string, object> {[ParameterType.Level.ToString()] = level};

            SendEvent(EventTypeAnalytics.AchievedLevel.ToString(), dict);
        }

        /*public static void SendEventItemPurchased(PurchaseType purchaseType, string currency, string price)
    {
        var dict = new Dictionary<string, object>
        {
            [ParameterType.ContentType.ToString()] = purchaseType.ToString(),
            [ParameterType.Currency.ToString()] = currency.ToString(),
            [ParameterType.Price.ToString()] = price
        };
​
        SendEvent(EventType.Purchased.ToString(), dict);           
    }*/

        public static void SendEventUsedClue(string Tint, long indexLevel)
        {
            var dict = new Dictionary<string, object>() {[Tint] = indexLevel};

            SendEvent(EventTypeAnalytics.UsedClue.ToString(), dict);
        }

        public static void SendEventUsedClue(string clueType)
        {
            var dict = new Dictionary<string, object>() {[ParameterType.Clue.ToString()] = clueType};

            SendEvent(EventTypeAnalytics.UsedClue.ToString(), dict);
        }

        public static void SendAdsEvent(AdsAnalyticsEventType adsType, Dictionary<string, object> dict)
        {
            SendEvent(adsType.ToString(), dict);
        }

        public static void SendAdsEvent(AdsAnalyticsEventType adsType, ParameterType parameterType, object parameter)
        {
            var dict = new Dictionary<string, object>() {[parameterType.ToString()] = parameter};
            SendAdsEvent(adsType, dict);
        }

        private static void SendEvent(string type, Dictionary<string, object> objects)
        {
#if UNITY_EDITOR
#else
            AppMetrica.Instance.ReportEvent(type, objects);
#endif
        }

    }

    public enum EventTypeAnalytics
    {
        Purchased,
        AchievedLevel,
        UsedClue
    }

    public enum ParameterType
    {
        Level,
        Count,
        Currency,
        ContentType,
        ContentId,
        Price,
        Clue,
        Placement,
        Error,
        Count_passed_levels
    }

    public enum AdsAnalyticsEventType
    {
        interstitial_start,
        interstitial_show,
        interstitial_failed,

        rewarded_start,
        rewarded_show,
        rewarded_failed,
        rewarded_closed,
    }
}