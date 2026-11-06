using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class TapSwitcher : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject circles;
    [SerializeField] private GameObject blackCircle, mintCenterCircle;
    [SerializeField] private CodeLockController _controller;
    private bool isActiveCanvasState = false;
    public void tapOnItem()
    {
        bool activate;
        float angle;
        if (isActiveCanvasState)
        {
            activate = false;
            angle = 0;
            canvas.SetActive(false);
            blackCircle.transform.DORotate(new Vector3(0, angle, 0), 0.2f).SetEase(Ease.Linear);
            transform.DOMoveX(0f, 0.2f).SetEase(Ease.Linear);
            mintCenterCircle.transform.DORotate(new Vector3(0, angle, 0), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                circles.SetActive(true);
            });
            isActiveCanvasState = activate;
        }
        else
        {
            activate = true;
            angle = 180f;
            circles.SetActive(false);
            blackCircle.transform.DORotate(new Vector3(0, angle, 0), 0.2f).SetEase(Ease.Linear);
            transform.DOMoveX(-0.597f, 0.2f).SetEase(Ease.Linear);
            mintCenterCircle.transform.DORotate(new Vector3(0, angle, 0), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                canvas.SetActive(true);
            });
            isActiveCanvasState = activate;
            _controller.ResetButtons();
        }

        Context.SoundPlayer.PlaySoundClick("turn_move_1");
    }
    
}
