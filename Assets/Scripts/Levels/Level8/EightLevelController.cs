using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class EightLevelController : MonoBehaviour
{
    public void moveItem(Vector2 pos, GameObject parent)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.35f).SetEase(Ease.Linear);
        attachToParentObject(parent);
    }

    public void attachToParentObject(GameObject parent)
    {
        transform.SetParent(parent.transform);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
