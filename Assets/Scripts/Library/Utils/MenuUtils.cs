using System.Collections;
using System.Collections.Generic;
using App.Global;
using MenuScripts;
using Swimmer.Library;
using Swimmer.Library.Common;
using Swimmer.Library.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUtils : MonoBehaviour
{
   public static void OpenMainMenuOrSelectLevelDialog()
   {
       if (PlayerPrefs.GetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL) == 1)
       {
           MintDialogsContainer._instance.ShowSelectLevelsDialog();
       }
       else if(PlayerPrefs.GetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL) == 0)
       {
           MintDialogsContainer._instance.HideSelectLevel();
       }
   }

   public static void BackMainMenuFromSelectLevelDialog()
   {
       if (SceneManager.GetActiveScene().buildIndex == 2)
       {
           GameObject.FindWithTag("MintBackground").SetActive(false);
           SceneManager.LoadScene("main_menu");
       }
       else
       {
           FindObjectOfType<AnimationController>().StartButtonsAnimation();
           FindObjectOfType<MainMenuController>().ActivateButtons();
       }
   }


   public static void LoadLevelSceneFromMainMenu()
   {
       PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 1);//number 1 is load last open level
   }
   
   public static void LoadLevelSceneFromSelectLevelDialog()
   {
       PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 0); //number 0 is load current level
       SceneManager.LoadScene("level_scene");
   }

   public static void ResetMenuLoadWithSelectDialog()
   {
       //Just open menu
       PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 1);//reset number to 1!!!!!!
   }
   
   
}
