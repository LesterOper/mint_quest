using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Dialogs;
using UnityEngine;

public class TintsHelper : Dialog
{
    public override void Hide()
    {
        base.Hide();
        FindObjectOfType<GameController>().ActivateOrNotPromptButton(true);
        Messenger.Broadcast(Messages.DEACT_BTN_BACK);
    }
}
