using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class PictureClicl : MonoBehaviour, IClickItem
{
    [SerializeField] private Animation _animation;
    public void tapOnItem()
    {
        _animation.Play();
    }
}
