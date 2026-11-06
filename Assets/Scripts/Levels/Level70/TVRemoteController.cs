using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class TVRemoteController : MonoBehaviour, IClickItem
{
    [SerializeField] private TVRemoteHelper _helper;
    public void tapOnItem()
    {
        _helper.CheckFiresClicked(gameObject);
    }
}
