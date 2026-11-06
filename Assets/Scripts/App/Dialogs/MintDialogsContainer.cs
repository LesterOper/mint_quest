using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Dialogs;
using UnityEngine;

public class MintDialogsContainer : DialogsContainer
{
    public static MintDialogsContainer _instance;
    [Space] [SerializeField] private SettingsDialog _settingsDioalog;
    [SerializeField] private SelectLevelDialog _selectLevelsDioalog;

    [SerializeField] private TintsHelper tints;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            base.Awake();
            _instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ShowSelectLevelsDialog()
    {
        var dialog = Instantiate(_selectLevelsDioalog, _parentRectTransform);
        ShowDialog(dialog.gameObject);
        dialog.GetComponent<RectTransform>().sizeDelta =
            dialog.GetComponent<RectTransform>().parent.GetComponent<RectTransform>().sizeDelta;
    }

    
    public void ShowSettingsDialog()
    {
        var dialog = Instantiate(_settingsDioalog, _parentRectTransform);
        ShowDialog(dialog.gameObject);
        dialog.GetComponent<RectTransform>().sizeDelta = dialog.GetComponent<RectTransform>().parent.GetComponent<RectTransform>().sizeDelta;
    }

    public void ShowTintsDialog()
    {
        var dialog = Instantiate(tints, _parentRectTransform);
        ShowDialog(dialog.gameObject);
        dialog.GetComponent<RectTransform>().sizeDelta = dialog.GetComponent<RectTransform>().parent.GetComponent<RectTransform>().sizeDelta;
    }

    public void HideSelectLevel()
    {
        _selectLevelsDioalog.GetComponent<Dialog>().Hide();
    }

    public void SetParent(RectTransform parent)
    {
        _parentRectTransform = parent;
        _dialogController = new DialogController(_parentRectTransform);
    }

    public void SetTintsPrefab(TintsHelper helper)
    {
        tints = helper;
    }
}