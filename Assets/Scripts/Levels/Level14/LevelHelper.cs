using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelHelper : MonoBehaviour
{
    [SerializeField] private List<Level_14> itemHandler;
    private List<Level_14> bufItem;
    private Level_14 currentItem;
    
    private void Start()
    {
        //FindObjectOfType<Volume>().gameObject.SetActive(false);
        Messenger.Broadcast(Messages.BACK_HINT_TO_MINT);
        bufItem = new List<Level_14>(itemHandler);
        currentItem = bufItem[0];
    }

    public void detectedCorrectTaps(Level_14 gameObject)
    {
        bufItem.Remove(gameObject);
        if (bufItem.Count == 0)
        {
            SpecialEffect();
        }
        else currentItem = bufItem[0];
    }

    public Level_14 getCurrentNumber()
    {
        return currentItem;
    }

    public void ResetButtons()
    {
        Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);
        itemHandler.ForEach(x => x.openForeground());
        bufItem = new List<Level_14>(itemHandler);
        currentItem = bufItem[0];
    }


    private void SpecialEffect()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}