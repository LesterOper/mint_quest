using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using UnityEngine;

public class InterstitalDetector : MonoBehaviour
{
    private int count = 0;
    private bool isAfter_30_Level = false;

    public bool IsAfter30Level
    {
        get => isAfter_30_Level;
        set => isAfter_30_Level = value;
    }

    public void CountLevelsToShowInterstitial()
    {
        count++;

        if (count == 5 && !isAfter_30_Level || count == 3 && isAfter_30_Level)
        {
            Context.AdsHelper.ShowInterstitial();
            Debug.Log("Interstitial");
            count = 0;
        }
    }

    public void ResetCountForInterstitial()
    {
        count = 0;
    }
}
