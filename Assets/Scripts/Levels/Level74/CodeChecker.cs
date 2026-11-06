using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using TMPro;

public class CodeChecker : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> allFields;
    [SerializeField] private List<GameObject> backInput;
    [SerializeField] private RectTransform numbers;
    [SerializeField] private KeyboardAnim anim;
    [SerializeField] private GameObject canvas;
    private List<string> correctPass = new List<string>() {"2", "3", "7", "0"};
    private List<string> inputPass = new List<string>();
    private GameObject currentField;

    private void Awake()
    {
        canvas.SetActive(true);
    }

    public void CheckPass(string num)
    {
        currentField.GetComponent<TextMeshProUGUI>().text = num;
        int count = allFields.Count(obj => obj.text != "");
        if (count == 4)
        {
            allFields.ForEach(obj =>
            {
                inputPass.Add(obj.text);
            });
            if (inputPass.SequenceEqual(correctPass))
            {
                gameObject.SetActive(false);
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                return;
            }
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

            allFields.ForEach(obj => { obj.rectTransform.DOShakeAnchorPos(0.6f, new Vector2(20f, 20f)); });
            inputPass.Clear();
            anim.OffAnim(0, 0.7f);
        }
        else
        {
            anim.OffAnim(0, 0.7f);
        }
    }

    public void SetCurrentField(GameObject current)
    {
        currentField = current;
    }
}