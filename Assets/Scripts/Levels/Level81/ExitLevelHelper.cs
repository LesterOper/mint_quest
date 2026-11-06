using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class ExitLevelHelper : MonoBehaviour
{
    private int count = 0;

    public void IncrementCount()
    {
        count++;
        if (count == 3)
        {
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }

}
