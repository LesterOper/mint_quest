using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;

public class LetterHelper : MonoBehaviour
{
    [SerializeField] private List<GameObject> letters;
    [SerializeField] private Vector2[] posWord;
    [SerializeField] private GameObject mint;
    [SerializeField] private GameObject canvas;
    [SerializeField] private SpriteRenderer mintBrain;
    private List<GameObject> bufLetters;
    private List<TextMeshProUGUI> correctLetters = new List<TextMeshProUGUI>();
    private string[] correctWord = new string[] {"m", "i", "n", "t", "b", "r", "a", "i", "n" };
    private int index = 0;

    private void Start()
    {
        bufLetters = new List<GameObject>(letters);
    }

    public bool CheckCorrect(TextMeshProUGUI letter)
    {
        Debug.Log(letter.text);
        if (letter.text == correctWord[index].ToUpper())
        {
            Debug.Log("Correct");
            index++;
            correctLetters.Add(letter);
            bufLetters.Remove(letter.gameObject);
            letter.gameObject.SetActive(false);
            if (index == correctWord.Length)
            {
                Effect();
            }
            return true;
        }

        index = 0;
        Context.SoundPlayer.PlaySoundClick("error_1", AudioMixerController.MasterMixerGroup.SFX, 1);

        ResetLetters();
        return false;
    }

    private void ResetLetters()
    {
        letters.ForEach(obj =>
        {
            obj.SetActive(true);
            obj.GetComponent<TextMeshProUGUI>().DOFade(1, 0.2f);
        });
        bufLetters = new List<GameObject>(letters);
        correctLetters.Clear();
    }

    private void Effect()
    {
        bufLetters.ForEach(obj =>
        {
            obj.SetActive(false);
        });
        
        
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < correctLetters.Count; i++)
        {
            correctLetters[i].gameObject.SetActive(true);
            correctLetters[i].DOFade(1, 0.1f);
            sequence.Join(correctLetters[i].GetComponent<RectTransform>().DOAnchorPos(posWord[i], 0.3f));
        }

        sequence.OnComplete(() =>
        {
            for (int i = 0; i < correctLetters.Count; i++)
            {
                correctLetters[i].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.4f).SetEase(Ease.Linear);
            }

            Invoke(nameof(Cool), 0.6f);
        });
    }

    private void Cool()
    {
        canvas.SetActive(false);
        mintBrain.DOFade(1, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            mint.transform.DOScale(new Vector3(10f, 10f, 10f), 0.8f).OnComplete(() =>
            {
                Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            });
        });


    }
}
