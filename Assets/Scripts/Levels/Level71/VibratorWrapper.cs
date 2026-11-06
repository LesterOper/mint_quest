using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VibratorWrapper
{
#if UNITY_ANDROID
    static AndroidJavaObject vibrator = null;
#endif
    static VibratorWrapper()
    {
#if UNITY_ANDROID
        var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityPlayerActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        vibrator = unityPlayerActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
    }

    public static bool HasVibrator()
    {
#if UNITY_ANDROID
        return vibrator.Call<bool>("hasVibrator");
#else
        return false;        
#endif
    }
    
    public static void Vibrate(float time)
    {
#if UNITY_ANDROID
        Vibrate(FloatToLongTime(time));
#endif
    }
    
    public static void Vibrate(long time)
    {
#if UNITY_ANDROID
        if (HasVibrator()) vibrator.Call("vibrate", time);
#endif
    }
    
    static long FloatToLongTime(float time)
    {
        time *= 1000f;
        return (long)time;
    }
}
