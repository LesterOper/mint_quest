using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremiumMenu : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _noAdsButton;
    [SerializeField] private Button _activeTipsButton;
    [SerializeField] private Button _supportButton;
    [Space] 
    [SerializeField] private GameObject _premMenu;
    [SerializeField] private GameObject _settingsMenu;

    void Start()
    {
        _closeButton.onClick.AddListener((() =>
        {
            FindObjectOfType<SettingsDialog>().GetComponent<Animation>().Play("settings_close_prem_anim");
        }));
    }

    
}
