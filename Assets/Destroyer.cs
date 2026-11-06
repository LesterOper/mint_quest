using System;
using System.Collections;
using System.Collections.Generic;
using Swimmer.Library;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const string PREFIX_LEVEL = "Level";
    private int CurrentLevelInx = 0;
    private int NewLevel = 0;
    private void OnEnable()
    {
        Messenger.AddListener(AppConstans.CLEAR_LEVEL, ClearLevel);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(AppConstans.CLEAR_LEVEL, ClearLevel);
    }

    private void Start()
    {
        CurrentLevelInx = LevelUtils.GetCurrentLevel();
        NewLevel = CurrentLevelInx + 1;
        
        Debug.Log("OurLevel - " + CurrentLevelInx);
        Debug.Log("NewLevel - " + NewLevel);
    }

    public void ClearLevel()
    {
        Debug.Log("OurLevel - " + CurrentLevelInx);
        Debug.Log("NewLevel - " + NewLevel);
        
        //if(LevelUtils.GetCurrentLevel() != CurrentLevelInx && gameObject.name == PREFIX_LEVEL + " " + CurrentLevelInx + "(Clone)")
            Destroy(gameObject);
    }
}
