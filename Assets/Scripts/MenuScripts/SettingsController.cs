using System.Collections;
using System.Collections.Generic;
using App;
using MenuScripts;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Button btnBackToMenu;
    [SerializeField] private Button btnSwitcherMusic;
    [SerializeField] private Button btnSwitcherVibration;
    [SerializeField] private Button btnPremium;
    [SerializeField] private GameObject mainMenu;
    private UnityAction _action;
    void Start()
    {
        btnBackToMenu.onClick.AddListener(() =>
        {
            ChangeActiveObjects();
        });
    }
    
    private void DoMintEffectPassing()
    {
    }

    private void ChangeActiveObjects()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
