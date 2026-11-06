using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickGlassController : MonoBehaviour, IClickItem, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private DragScreen _dragScreen;
    [SerializeField] private GameObject detectorScreen;
    [SerializeField] private GameObject canvasWithNumbers;
    [SerializeField] private GameObject bluryNumbers, normalNumbers;

    public void tapOnItem()
    {
        /*FindObjectOfType<BlurNumberLevel>().ChangeNumbersToNormal();
        GetComponent<SpriteRenderer>().DOFade(0, 0.4f);
        _dragScreen.CAMERA.transform.DOMoveX(0, 0.6f);
        _dragScreen.BACKGROUND.transform.DOMoveX(0, 0.6f).OnComplete(() =>
        {
            detectorScreen.SetActive(false);
            _dragScreen.enabled = false;
        });
        EnableColliderNumbers();
        Invoke(nameof(ActivateCanvas), 0.7f);*/
    }

    private void ActivateCanvas()
    {
        canvasWithNumbers.SetActive(true);
    }

    public void DeactivateCanvas()
    {
        canvasWithNumbers.SetActive(false);
    }

    private void EnableColliderNumbers()
    {
        var a = FindObjectsOfType<ClickCode>();
        foreach (var clickCode in a)
        {
            clickCode.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bluryNumbers.SetActive(false);
        normalNumbers.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bluryNumbers.SetActive(true);
        normalNumbers.SetActive(false);
    }
}
