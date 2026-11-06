using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level_28 : MonoBehaviour
{
    private float minAcceleration = 1f;
    private bool isWork = true;
    private void Update()
    {
        if (!isWork) return;
        var dirY = Input.acceleration.y;

        if (dirY >= minAcceleration)
        {
            Invoke(nameof(Complete), 0.5f);
            isWork = false;
        }
    }

    private void Complete()
    {
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
