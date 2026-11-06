using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;


public class WindowOrderer : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> windows;
    private SpriteRenderer currentWindow;
    private List<SpriteRenderer> bufWindow;

    private void Start()
    {
        ShuffleWindow();
        bufWindow = new List<SpriteRenderer>(windows);
        currentWindow = bufWindow[0];
    }

    private void ShuffleWindow()
    {
        for (int i = 0; i < windows.Count; i++)
        {
            int j = Random.Range(i, windows.Count);
            var temp = windows[j];
            windows[j] = windows[i];
            windows[i] = temp;
        }
    }

    public bool Check(SpriteRenderer renderer)
    {
        if (renderer.gameObject.name == currentWindow.gameObject.name)
        {
            bufWindow.RemoveAt(0);
            
            if (bufWindow.Count == 0)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return true;
            }
            else
            {
                currentWindow = bufWindow[0];
                return true;
            }
        }
        Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

        bufWindow = new List<SpriteRenderer>(windows);
        currentWindow = bufWindow[0];
        ResetAllWindows();
        return false;
    }

    private void ResetAllWindows()
    {
        windows.ForEach(obj => { obj.GetComponent<WindowClicker>().ChangeSprite(); });
    }
}