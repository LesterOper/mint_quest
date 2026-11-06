using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;

public class TaxiHelper : MonoBehaviour
{ 
    [SerializeField] private List<GameObject> correctOrder;
    [SerializeField] private List<SpriteRenderer> otherBtns;
    [SerializeField] private List<TextMeshProUGUI> numbers;
    [SerializeField] private Sprite mint;
    [SerializeField] private Sprite active;
    private List<TextMeshProUGUI> bufNumbers;
    private List<SpriteRenderer> bufOtherBtns;
    private List<GameObject> bufCorrectOrder;
    private GameObject currentBtton;
    

    private void Start()
    {
        bufCorrectOrder = new List<GameObject>(correctOrder);
        bufOtherBtns = new List<SpriteRenderer>(otherBtns);
        bufNumbers = new List<TextMeshProUGUI>(numbers);
        currentBtton = bufCorrectOrder[0];
    }

    public void Checker(GameObject obj)
    {
        if (obj == currentBtton)
        {
            bufNumbers[0].DOFade(0, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                bufNumbers.RemoveAt(0);
                bufCorrectOrder[0].GetComponent<SpriteRenderer>().sprite = mint;
                obj.GetComponent<CircleCollider2D>().enabled = false;
                bufCorrectOrder.RemoveAt(0);
                
                if (bufCorrectOrder.Count == 0)
                {
                    Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                    return;
                }

                currentBtton = bufCorrectOrder[0];
            });
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

            ResetBtns();
        }
    }

    private void ResetBtns()
    {
        
        numbers.ForEach(obj => { obj.DOFade(1, 0.25f).SetEase(Ease.Linear); });
        bufNumbers = new List<TextMeshProUGUI>(numbers);
        bufCorrectOrder = new List<GameObject>(correctOrder);
        bufCorrectOrder.ForEach(obj =>
        {
            obj.GetComponent<SpriteRenderer>().sprite = active;
            obj.GetComponent<CircleCollider2D>().enabled = true;
        });
        currentBtton = bufCorrectOrder[0];
    }
}
