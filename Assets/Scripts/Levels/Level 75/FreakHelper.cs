using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class FreakHelper : MonoBehaviour
{
    private int count = 0;

    public void Count()
    {
        count++;

        if (count == 2)
        {
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    } 
}
