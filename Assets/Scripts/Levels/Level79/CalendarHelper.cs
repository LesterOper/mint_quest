using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CalendarHelper : MonoBehaviour
{
    [SerializeField] private GameObject listPrefab;
    [SerializeField] private TextMeshProUGUI input;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject canvas;
    private GameObject currentList;
    private List<string> month;
    private int correctNum;
    private int index = 0;
    public bool isEnableToCheck = false;

    private void Start()
    {
        RandomOrderMonth();
        correctNum = month.FindIndex(obj => obj == "October") + 1;
        CreateNextList();
    }

    private void RandomOrderMonth()
    {
        month = new List<string>();
        bool isOctoberAdd = false;
        for (int i = 0; i < 30; i++)
        {
            int rand = Random.Range(0, 10);
            if (rand > 8 && !isOctoberAdd)
            {
                month.Add("October");
                isOctoberAdd = true;
            }
            else
            {
                month.Add("September");
            }
        }
    }


    public void InputNumbers(string num)
    {
        if (input.text.Length == 2)
        {
            input.text = "";
            return;
        }

        input.text += num;
    }
    
    
    public void CheckInputValue()
    {
        if (input.text == correctNum.ToString())
        {
            canvas.SetActive(false);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

            input.transform.DOShakePosition(1, new Vector3(20, 20, 20)).OnComplete(() => { input.text = ""; });
        }

        isEnableToCheck = false;
    }

    public void CreateNextList()
    {
        Destroy(currentList, 1f);
        currentList = Instantiate(listPrefab);
        RectTransform main = currentList.GetComponent<RectTransform>();
        RectTransform par = parent.GetComponent<RectTransform>();
        main.SetParent(canvas.transform);
        main.anchoredPosition = par.anchoredPosition;
        main.SetParent(parent.transform);
        main.anchoredPosition = new Vector2(main.anchoredPosition.x, -25f);
        //main.localPosition = new Vector3(0, 0, 0);
        currentList.GetComponent<SwipeListCalendar>().SetDay((index + 1).ToString());
        currentList.GetComponent<SwipeListCalendar>().SetMonth(month[index]);
        index++;
        if (index == 30)
            index = 0;
    }
}