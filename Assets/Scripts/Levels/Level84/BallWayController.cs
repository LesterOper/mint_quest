using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class BallWayController : MonoBehaviour
{
    [SerializeField] private ClickController left, up, right;
    private List<string> wayPoints = new List<string>() {"RD", "R", "U", "RD", "L"};
    private string currentDir;
    private int index = 0;
    private int count = 0;

    private void Start()
    {
        currentDir = wayPoints[index];
        left.enabled = right.enabled = up.enabled = false;
    }

    public bool Move(string dir, float posX, float posY)
    {
        if (currentDir == dir)
        {
            DeactivateInteractable();
            switch (currentDir)
            {
                case "R":
                    transform.DOMoveX(posX, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        InvokeActivate();
                    });
                    break;
                case "RD":
                    transform.DOMoveX(posX, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            transform.DOMoveY(posY, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                InvokeActivate();
                            });
                        });
                    break;
                case "U":
                    transform.DOMoveY(posX, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        InvokeActivate();
                    }); 
                    break;
                case "L":
                    transform.DOMoveX(posX, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        InvokeActivate();
                    }); 
                    break;
            }

            index++;
            if (index == wayPoints.Count)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return true;
            }
            currentDir = wayPoints[index];
            return true;
        }

        return false;
    }

    public void Check()
    {
        Debug.Log("MOVED");
        count++;
        if (count == 3)
        {
            Invoke(nameof(InvokeActivate), 0.4f);
        }
    }

    private void InvokeActivate()
    {
        left.enabled = true;
        up.enabled = true;
        right.enabled = true;
    }

    public void DeactivateInteractable()
    {
        left.enabled = up.enabled = right.enabled = false;
    }
}
