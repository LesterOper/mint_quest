using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "level_items", menuName = "App/ItemsOfLevel")]
public class LevelItemsContainer : ScriptableObject
{
    [SerializeField] private List<LevelItems> items;

    public GameObject GetItem(int index)
    {
        var gettingObject = items.FirstOrDefault(setup => setup.INDEX == index);
        return gettingObject.OBJECT;
    }
}

[Serializable]
public class LevelItems
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int index_level;

    public int INDEX => index_level;
    public GameObject OBJECT => prefab;
}
