using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class AqualizerHelper : MonoBehaviour
{
    [SerializeField] private List<AqulizerController> _controllers;
    private int count = 0;

    public void CheckCount()
    {
         count = _controllers.Count(obj => obj.isCorrectPos);
         if (count == 6)
         {
             Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
         }
    }
}
