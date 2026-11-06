using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsDeformationManager : MonoBehaviour
{
    public static void ScaleObject(float amount, Transform targetObj, float time)
    {
        targetObj.DOScale(amount, 0.15f).SetEase(Ease.Linear);
    }
    
    public static void ScaleObject(float amount, RectTransform targetObj)
    {
        targetObj.DOScale(amount, 0.15f).SetEase(Ease.Linear);
    }

    public static void FadeInOut(SpriteRenderer targetObj, float endValue, float time)
    {
        targetObj.DOFade(endValue, time).SetEase(Ease.Linear);
    }

    public static void FadeInOut(Image targetObj, float endValue, float time)
    {
        targetObj.DOFade(endValue, time).SetEase(Ease.Linear);
    }

    public static Tween Shake(Transform transform)
    {
        return transform.DOShakePosition(2f, 0.035f, 3).SetLoops(-1, LoopType.Restart);
    }
}
