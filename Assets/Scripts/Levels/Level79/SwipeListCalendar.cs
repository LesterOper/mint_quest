using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeListCalendar : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private CalendarHelper _calendarHelper;
    [SerializeField] private TextMeshProUGUI day, month;
    [SerializeField] private Animation _animation;


    private void Start()
    {
        GetComponent<Image>().DOFade(1, 0.15f).SetEase(Ease.Linear);
        day.DOFade(1, 0.15f).SetEase(Ease.Linear);
        month.DOFade(1, 0.15f).SetEase(Ease.Linear);
        _calendarHelper = FindObjectOfType<CalendarHelper>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var delta = eventData.delta;
        if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
        {
            if (delta.y < 0)
            {
                //Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                Context.SoundPlayer.PlaySoundClick("paper_1", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                _animation.Play();
                Invoke(nameof(InvokeDuringAnim), 0.25f);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    private void InvokeDuringAnim()
    {
        _calendarHelper.CreateNextList();
    }
    
    public void SetDay(string day)
    {
        this.day.text = day;
    }

    public void SetMonth(string month)
    {
        this.month.text = month;
    }
}
