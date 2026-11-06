using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonsController : MonoBehaviour
{
    private int _countBaloon = 3;
    private Level38Controller _level38Controller = null;

    public void DecreaseBaloon()
    {
        _countBaloon--;
        Invoke(nameof(StartCountdownToRestart), 1f);
        if (_countBaloon <= 0)
        {
            StartCountdownToRestart();
        }
    }

    private void StartCountdownToRestart()
    {
        CancelInvoke("StartCountdownToRestart");
        FindObjectOfType<GameController>().RestartLevel();
    }

}
