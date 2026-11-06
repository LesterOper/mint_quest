using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using UnityEngine;

public class LevelEmpty : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}
