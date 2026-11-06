using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using Swimmer;
using Swimmer.Library;
using TMPro;
using UnityEngine;

public class CounterHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private GameObject canvas;
    private int counting = 0;
    public void CountExpression(int num)
    {
        StartCoroutine(Count(num));
    }

    private IEnumerator Count(int num)
    {
        int bufNum;
        for (int i = 0; i < Math.Abs(num); i++)
        {
            bufNum = num < 0 ? -1 : 1;

            counting += bufNum;
            counter.text = counting.ToString();
            yield return new WaitForSeconds(0.01f);
        }

        if (counting == 5)
        {
            canvas.SetActive(false);
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
