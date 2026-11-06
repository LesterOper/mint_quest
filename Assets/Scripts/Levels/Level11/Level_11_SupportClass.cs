using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class Level_11_SupportClass : MonoBehaviour
{
    [SerializeField] private List<GameObject> spritesActive;

    private List<InfoShapeButtons> correct;
    private List<GameObject> currentActive;
    private GameController _controller;
    private Level_11 Level11;
    private int index = 0;

    private void Awake()
    {
        _controller = FindObjectOfType<GameController>();
        currentActive = new List<GameObject>();
        correct = new List<InfoShapeButtons>();
        correct.Add(new InfoShapeButtons(0, "circle"));
        correct.Add(new InfoShapeButtons(3, "triangle"));
        correct.Add(new InfoShapeButtons(4, "square"));
        correct.Add(new InfoShapeButtons(5, "fiverect"));
        correct.Add(new InfoShapeButtons(6, "sixrect"));
        correct.Add(new InfoShapeButtons(8, "eightrect"));
    }

    public bool isCorrect(GameObject gameObject)
    {
        return correct[index].COUNT == gameObject.GetComponent<Level_11>().COUNT;
    }

    public void addToCurrentActive(GameObject gameObject)
    {
        currentActive.Add(gameObject);
        if (isWin())
        {
            specialEffect();
            Invoke(nameof(mintEffect), 1.40f);
        }

        index++;
    }

    public void DeActivateAllObjects()
    {
        if (currentActive.Count != 0)
        {
            currentActive.ForEach(obj => { obj.GetComponent<Level_11>().DeActive(); });
            currentActive.Clear();
            index = 0;
        }
    }

    private bool isWin()
    {
        return correct.Count == currentActive.Count;
    }

    private void specialEffect()
    {
        Sequence sequence = DOTween.Sequence();
        spritesActive.ForEach(obj =>
        {
            sequence.Append(obj.GetComponent<SpriteRenderer>().DOFade(0, 0.12f).SetEase(Ease.Linear));
            sequence.Append(obj.GetComponent<SpriteRenderer>().DOFade(1, 0.12f).SetEase(Ease.Linear));
        });
        spritesActive.ForEach(obj =>
        {
            sequence.Append(obj.GetComponent<SpriteRenderer>().DOFade(0, 0.12f).SetEase(Ease.Linear));
            sequence.Append(obj.GetComponent<SpriteRenderer>().DOFade(1, 0.12f).SetEase(Ease.Linear));
        });

        sequence.SetEase(Ease.Linear);
    }
    
    private void mintEffect()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}


class InfoShapeButtons
{
    private int countCorners;
    private string name;

    public InfoShapeButtons(int count, string name)
    {
        this.countCorners = count;
        this.name = name;
    }

    public int COUNT => countCorners;
    public string NAME => name;
}