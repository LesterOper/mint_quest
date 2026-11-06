using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using UnityEngine;

public  class LevelUtils : MonoBehaviour
{
    public static int GetCurrentLevel()
    {
        if ((int) Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL) != 0)
        {
            return (int) Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL);
        }
        else
        {
            SetCurrentLevel(1);
            return 1;
        }
    }

    public static int GetCurrentLevel(bool flag = false)
    {
        return (int)Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL);
    }
    
    public static void SetCurrentLevel(int level)
    {
         Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, level);
    }
    
    public static void AppendLevelnumber()
    {
        if (Context.DataHelper.GetLong(AppConstans.LAST_OPEN_LEVEL_INDEX) ==
            Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL))
        {
            Debug.Log(Context.DataHelper.GetLong(AppConstans.LAST_OPEN_LEVEL_INDEX) + "   " + Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL));
            var index = (int)Context.DataHelper.GetLong(AppConstans.CURRENT_LEVEL);
            SetLastOpenLevel(index + 1);
            SetLevelOpenStatus(index + 1);
        }
        Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, GetCurrentLevel() + 1);
    }
    
    public static void SetLastOpenLevel(int index)
    {
        Context.DataHelper.SetLong(AppConstans.LAST_OPEN_LEVEL_INDEX, index);
    }
    
    public static int GetLastOpenLevel()
    {
        if ((int) Context.DataHelper.GetLong(AppConstans.LAST_OPEN_LEVEL_INDEX) != 0)
        {
            return (int) Context.DataHelper.GetLong(AppConstans.LAST_OPEN_LEVEL_INDEX);
        }
        else
        {
            SetLastOpenLevel(1);
            return 1;
        }
    }
    
    public static void SetLevelOpenStatus(int index)
    {
        Context.DataHelper.SetLong(AppConstans.LEVEL_STATUS + index, 1);
    }
    
    public static bool IsLevelOpen(int index)
    {
        if (Context.DataHelper.GetLong(AppConstans.LEVEL_STATUS + index) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static int WhichLevelToLoad()
    {
        if (PlayerPrefs.GetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL) == 1)
        {
            return GetLastOpenLevel();
        }
        else
        {
            return GetCurrentLevel();
        }
    }
}
