using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Level3Anim : MonoBehaviour, IAnimate
{    
    [SerializeField] private GameObject[] levelObjects;
    [SerializeField] private bool isWithUIElements;
    public void setLevelAnim()
    {
        Sequence sequence = DOTween.Sequence();
        foreach (GameObject levelObject in levelObjects)
        {
            sequence.Join(levelObject.GetComponent<SpriteRenderer>().DOFade(1, 2f).SetEase(Ease.Linear));
        }
    }

    public void setLevelAnimWithUI()
    {
        Sequence sequence = DOTween.Sequence();
        for(int i=0; i<levelObjects.Length; i++)
        {
            if (i == levelObjects.Length - 1)
            {
                sequence.Join(levelObjects[i].GetComponent<TextMeshProUGUI>().DOFade(1, 2f).SetEase(Ease.Linear));
            }
            else
                sequence.Join(levelObjects[i].GetComponent<SpriteRenderer>().DOFade(1, 2f).SetEase(Ease.Linear));
        }
    }
    
    public void setupAnim()
    {
        if (isWithUIElements)
        {
            setLevelAnimWithUI();
        }
        else
            setLevelAnim();
    }
}
