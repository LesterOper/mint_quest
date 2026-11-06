using System;
using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.Serialization;

public class ContextManager : Context
{
    [SerializeField] private LevelItemsContainer levelObjectPoll;
    private static ContextManager instance;

    private void Awake()
    {
       base.Awake();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public LevelItemsContainer getPool()
    {
        return levelObjectPoll;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 0);
    }
}