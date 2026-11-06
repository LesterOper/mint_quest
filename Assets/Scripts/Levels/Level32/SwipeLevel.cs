using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Global;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeLevel : MonoBehaviour
{
    [SerializeField] private List<GameObject> all;
    private int count = 0;
    public void Increment()
    {
        count++;
        if (count == 3)
        {
            all.ForEach(obj =>
            {
                obj.SetActive(false);
            });
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
