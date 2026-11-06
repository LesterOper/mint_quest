using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class MirrorClickHelper : MonoBehaviour
{
    private GameObject currentPair;
    private int countPair = 0;
    public bool CheckPair(GameObject obj, GameObject pair)
    {
        if (currentPair == null)
        {
            currentPair = pair;
            return true;
        }
        if (currentPair == obj)
        {
            currentPair = null;
            countPair++;
            if (countPair == 9)
            {
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            }

            return true;
        }
        Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

        ResetItems();
        return false;
    }

    private void ResetItems()
    {
        var items = new List<MirrorClickController>(FindObjectsOfType<MirrorClickController>());
        items.ForEach(obj =>
        {
            obj.GetComponent<PolygonCollider2D>().enabled = true;
            obj.GetComponent<SpriteRenderer>().DOFade(1, 0.15f);
        });
        currentPair = null;
        countPair = 0;
    }
}