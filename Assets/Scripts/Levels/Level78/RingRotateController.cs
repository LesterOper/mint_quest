using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class RingRotateController : MonoBehaviour
{
    [SerializeField] private List<GameObject> allLights;
    [SerializeField] private List<GameObject> correctLights;
    [SerializeField] private Sprite active, nonActive;
    [SerializeField] private List<GameObject> pushedLights;
    private List<GameObject> currentLights;
    private GameObject currentActiveLight;

    private float nextRand = 1f;

    private void Start()
    {
        currentLights = new List<GameObject>(allLights);
        currentActiveLight = pushedLights[0];
        Invoke(nameof(Rotate), 0.5f);
    }

    private void Update()
    {
        nextRand -= Time.deltaTime;
        if (nextRand <= 0)
        {
            RandomActiveLights();
            nextRand = 2f;
        }
    }

    private void Rotate()
    {
        transform.DORotate(new Vector3(0, 0, 360f), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void RandomActiveLights()
    {
        int rand = 0;
        bool isTrue = false;
        if (!pushedLights.Contains(currentActiveLight))
        {
            currentActiveLight.GetComponent<SpriteRenderer>().sprite = nonActive;
        }

        while (!isTrue)
        {
            rand = Random.Range(0, allLights.Count-1);
            currentActiveLight = allLights[rand];
            if (!pushedLights.Contains(currentActiveLight))
            {
                isTrue = true;
            }
        }
        currentActiveLight.GetComponent<SpriteRenderer>().sprite = active;
    }

    public void TryToPushLight()
    {
        if (pushedLights.Contains(currentActiveLight)) return;
           
        pushedLights.Add(currentActiveLight);
        if (pushedLights.Count == correctLights.Count)
        {
            if (isEqual())
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return;
            }
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

            ResetLights();
        }
    }

    private bool isEqual()
    {
        bool isEqual = true;

        foreach (var push in pushedLights)
        {
            if (!correctLights.Contains(push))
            {
                isEqual = false;
                break;
            }
        }

        return isEqual;
    }

    private void ResetLights()
    {
        var lt = pushedLights[0];
        pushedLights.RemoveAt(0);
        pushedLights.ForEach(obj => { obj.GetComponent<SpriteRenderer>().sprite = nonActive; });
        pushedLights.Clear();
        pushedLights.Add(lt);
    }
}