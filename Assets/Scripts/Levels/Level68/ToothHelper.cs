using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ToothHelper : MonoBehaviour
{
    [SerializeField] private List<ToothClicker> teeth;
    [SerializeField] private List<GameObject> correctTeeth;

    private List<GameObject> clickedTooth = new List<GameObject>();
    
    public void CheckCorrectTooth(GameObject tooth)
    {
        if (!clickedTooth.Contains(tooth))
            clickedTooth.Add(tooth);
        if (clickedTooth.Count == correctTeeth.Count)
        {
            if (IsEquals())
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            }
            else
            {
                Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

                teeth.ForEach(obj => { obj.ResetTooth(); });
                clickedTooth.Clear();
            }
        }
    }
    
    private bool IsEquals()
    {
        bool isEqual = true;
        for (int i = 0; i < clickedTooth.Count; i++)
        {
            if (!correctTeeth.Contains(clickedTooth[i]))
            {
                isEqual = false;
                break;
            }
        }

        return isEqual;
    }
}
