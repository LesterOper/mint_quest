using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class FootprintHelper : MonoBehaviour
{
    [SerializeField] private List<GameObject> footprintsOrder;

    private GameObject currentFootprint;
    private GameObject prevFootPrint;
    private int index = 0;
    private void Start()
    {
        currentFootprint = footprintsOrder[index];
    }

    public bool FootprintsChecker(GameObject footprint)
    {
        if (currentFootprint == footprint)
        {
            if (index > 0)
            {
                if (prevFootPrint != footprintsOrder[index - 1]) return false;
            }
            index++;
            if (index == footprintsOrder.Count)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return true;
            }
            currentFootprint = footprintsOrder[index];
            prevFootPrint = footprintsOrder[index - 1];
            return true;
        }

        return false;
    }

    public void ResetFoot()
    {
        index = 0;
        currentFootprint = footprintsOrder[index];
    }
}
