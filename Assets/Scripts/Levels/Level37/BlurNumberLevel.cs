using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class BlurNumberLevel : MonoBehaviour
{
    public List<int> pressNumbers;
    private string correctCode = "1569";

    [SerializeField] private SpriteRenderer blury_numbers;
    [SerializeField] private SpriteRenderer numbers;
    [SerializeField] private GameObject victoryMess;
    [SerializeField] private ClickGlassController _clickGlassController;

    public void ChangeNumbersToNormal()
    {
        blury_numbers.DOFade(0, 1.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            numbers.gameObject.SetActive(true);
            numbers.DOFade(1, 1.2f).SetEase(Ease.Linear);
        });
    }

    public void AddToCode(int code)
    {
        pressNumbers.Add(code);
        if (pressNumbers.Count == 4)
        {
            if(IsCodeCorrect())
                CompleteEffect();
            else
            {
                Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);
                pressNumbers.Clear();
            }
        }
    }

    private bool IsCodeCorrect()
    {
        string code = "";
        pressNumbers.ForEach(num => code+=num);
        return code.Equals(correctCode);
    }

    private void CompleteEffect()
    {
        /*victoryMess.GetComponent<RectTransform>()
            .DOAnchorPosY(350f, 0.5f)
            .SetEase(Ease.OutBounce);*/
        Invoke(nameof(FullCompleteEffect), 0.3f);
    }

    private void FullCompleteEffect()
    {
        victoryMess.SetActive(false);
        _clickGlassController.DeactivateCanvas();
        Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
    }
}
