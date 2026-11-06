using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using MenuScripts;
using Swimmer.Library.Common;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestHelper : MonoBehaviour
{
    public int SelectedLVL = 1;
    public int MAX_LVL = 100;
    
    [Button]
    public void NextLvl()
    {
        //GameObject.Find("GameController").GetComponent<GameController>().DestroyCurrentLevel();
        GameObject.Find("GameController").GetComponent<GameController>().LoadNextLevel();
    }

    [Button]
    public void RestartLVL()
    {
        GameObject.Find("GameController").GetComponent<GameController>().RestartLevel();
    }

    [Button]
    public void StartLVL()
    {
        //GameObject.Find("GameController").GetComponent<GameController>().DestroyCurrentLevel();
        Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, SelectedLVL-1);
        GameObject.Find("GameController").GetComponent<GameController>().LoadNextLevel();
    }

    [Button]
    public void CloseAllLVLs()
    {
        Debug.Log("CloseAllLVLs");
        
        for (int i = 2; i < MAX_LVL; i++)
        {
            Context.DataHelper.SetLong(AppConstans.LEVEL_STATUS + i, 0);
        }
        Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, 1);
        Context.DataHelper.SetLong(AppConstans.LAST_OPEN_LEVEL_INDEX, 1);
        FindObjectOfType<MainMenuController>().SetCurrentLevelTextField();
        
        Debug.Log("SceneManager.GetActiveScene().name = "+SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "main_menu")
        {
            DebugUIRefreshCurrentLvl();
        }
    }

    [Button]
    public void OpenAllLvls()
    {
        Debug.Log("OpenAllLvls");
        for (int i = 1; i < MAX_LVL; i++)
        {
            Context.DataHelper.SetLong(AppConstans.LEVEL_STATUS + i, 1);
        }
        Context.DataHelper.SetLong(AppConstans.LAST_OPEN_LEVEL_INDEX, 100);
        Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, MAX_LVL);
        Debug.Log("SceneManager.GetActiveScene().name = "+SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "main_menu")
        {
            DebugUIRefreshCurrentLvl();
        }
    }

    public void DebugBtnNextLvl()
    {
        int currentLVL = (int)Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL);
        Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, currentLVL+1);
        DebugUIRefreshCurrentLvl();
    }
    
    public void DebugBtnPreviousLvl()
    {
        int currentLVL = (int)Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL) -1 ;
        if (currentLVL<1)
        {
            currentLVL = 100;
        }
        Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, currentLVL);
        DebugUIRefreshCurrentLvl();
    }

    public void DebugBtnLoadLVL()
    {
        PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 0); //number 0 is load current level
        SceneManager.LoadScene("level_scene");
    }
    
    public void DebugUIRefreshCurrentLvl()
    {
Debug.Log((Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL)).ToString());
        GameObject.Find("DebugCurrentSelectedLvl").GetComponent<Text>().text = (Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL)).ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SceneManager.GetActiveScene().name = "+SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "main_menu")
        {
            DebugUIRefreshCurrentLvl();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
