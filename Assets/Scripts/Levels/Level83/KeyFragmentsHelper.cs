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

public class KeyFragmentsHelper : MonoBehaviour
{
    [SerializeField] private List<GameObject> fragments;
    [SerializeField] private List<GameObject> correctFragments;
    [SerializeField] private GameObject spriteMask;
    [SerializeField] private SpriteRenderer circle;
    private List<GameObject> dragedFragments = new List<GameObject>();
    private List<Vector3> startPos = new List<Vector3>();
    private int count = 0;

    private void Start()
    {
        fragments.ForEach(obj =>
        {
            startPos.Add(obj.transform.position);
        });
    }

    public void CheckDragedFragment(GameObject obj)
    {
        count++;
        dragedFragments.Add(obj);
        if (count == 2)
        {
            if (IsEquals())
            {
                Effect();
            }
            else
            {
                Context.SoundPlayer.PlaySoundClick("error_1", AudioMixerController.MasterMixerGroup.SFX, 1);

                ResetFragments();
            }
        }
    }

    private void ResetFragments()
    {
        spriteMask.SetActive(false);
        for (int i = 0; i < fragments.Count; i++)
        {
            fragments[i].transform.DOMove(startPos[i], 0.15f).SetEase(Ease.Linear);
        }
        dragedFragments.Clear();
        count = 0;
    }

    private void Effect()
    {
        /*dragedFragments.ForEach(obj =>
        {
            obj.GetComponent<KeyFragmentsController>().Effect();
        });*/
        
        dragedFragments.ForEach(obj =>
        {
            obj.GetComponent<SpriteRenderer>().DOFade(0, 0.25f).SetEase(Ease.Linear);
            obj.transform.DOScale(0, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(ScaleCircle), 0.15f);
            });
        });
    }

    private void ScaleCircle()
    {
        circle.DOFade(1, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            circle.transform.DOScale(new Vector3(3, 3, 3), 0.15f).SetEase(Ease.Linear);
            Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
        });
    }

    private bool IsEquals()
    {
        bool isEqual = true;
        for (int i = 0; i < correctFragments.Count; i++)
        {
            if (!correctFragments.Contains(dragedFragments[i]))
            {
                isEqual = false;
                break;
            }
        }

        return isEqual;
    }
}
