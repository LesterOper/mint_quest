using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class PuzzleHelper : MonoBehaviour
{
    private List<PuzzleItemController> items;
    private int count = 0;

    private void Start()
    {
        items = new List<PuzzleItemController>(FindObjectsOfType<PuzzleItemController>());
    }

    public void AttachToPuzzle()
    {
        count = items.Count(obj => obj.IsCorrectPos);
        
        if (count == 4)
        {
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
