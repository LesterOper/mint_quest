using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject parent;
    [SerializeField] private string name;
    [SerializeField] private GameObject box;
    [SerializeField] private EnumNeedAttach _attach;
    private GameController _game;
    
    private void Start()
    {
        _game = FindObjectOfType<GameController>();
    }

    public void moveItem(Vector2 pos)
    {
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        if (_attach == EnumNeedAttach.Attach)
        {
            attachToParentObject();
        }
    }

    public void attachToParentObject()
    {
        transform.SetParent(parent.transform);
        GetComponent<PolygonCollider2D>().enabled = false;
        if (parent.transform.childCount == 5)
        {
            setupMint();
        }
        switch (parent.name)
        {
            case "iqos" : box.SetActive(true);
                break;
            case "box" :
                parent.GetComponent<BoxIqosContr>().changeState();
                gameObject.SetActive(false);
                _game.CoolButtonLevelCompleteEffect();
                break;
        }
    }

    private void setupMint()
    {
        /*var b = _game.getTintBtn();
        b.gameObject.SetActive(false);
        _game.changeSprites();*/
        _game.CoolButtonLevelCompleteEffect();
    }
}
