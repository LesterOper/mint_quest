using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class LevelEffectSetup : MonoBehaviour, IClickItem
{
    [SerializeField] private List<GameObject> masksList;
    [SerializeField] private AttachTwoCircle _attachTwoCircle;
    public void tapOnItem()
    {
        masksList.ForEach(obj => obj.SetActive(true));
        _attachTwoCircle.specialEffect();
    }
}
