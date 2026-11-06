using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class SpriderClicked : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject circle;
    public void tapOnItem()
    {
        var posY = transform.position.y;
        circle.transform.DOScale(new Vector3(3f, 3f, 3f), 0.4f).SetEase(Ease.Linear);
        transform.DOMoveY(posY - 1.2f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveY(posY + 7f, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                GetComponent<BoxCollider2D>().enabled = false;
                text.SetActive(false);
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            });
        });
    }
}
