using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class CubeTapped : MonoBehaviour, IClickItem
{
    [SerializeField] private CubeDecompozition decomp;
    public void tapOnItem()
    {
        decomp.DetectCorrect(gameObject);
    }
}
