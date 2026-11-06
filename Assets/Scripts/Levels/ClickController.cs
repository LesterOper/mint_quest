using System;
using System.Collections;
using System.Collections.Generic;
using App;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(IClickItem))]
public class ClickController : MonoBehaviour, IPointerClickHandler
{
    private Component spriteRendererComponent;
    public bool isClick;
    public bool isNeedToAffect = false;
    public bool isCool;
    public bool isNeedSoundClick;
    private Vector2 mousePos;
    private float scale;

    public Vector2 MousePos
    {
        get => mousePos;
        set => mousePos = value;
    }

    private void Start()
    {
        scale = transform.localScale.x;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*if (eventData.currentInputModule.input.touchCount == 1)
        {*/
            if (isCool)
            {
            }
            //Context.SoundPlayer.PlaySoundClick("cool_2");
            else if (!isNeedSoundClick)
                Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX,
                    PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));

            mousePos = Input.mousePosition;
            if (isNeedToAffect)
            {
                spriteRendererComponent = GetComponent<SpriteRenderer>();
                if (spriteRendererComponent == null)
                {
                    spriteRendererComponent = GetComponent<Image>();
                    ObjectsDeformationManager.FadeInOut((Image) spriteRendererComponent, 0.4f, 0.15f);
                }
                else
                {
                    ObjectsDeformationManager.FadeInOut((SpriteRenderer) spriteRendererComponent, 0.4f, 0.15f);
                }

                ObjectsDeformationManager.ScaleObject(scale / 1.2f, transform, 0.15f);
                Invoke(nameof(Execute), 0.15f);
            }
            else
                Execute();

            isClick = true;
        }
   // }

    private void Execute()
    {
        if (isNeedToAffect)
        {
            spriteRendererComponent = GetComponent<SpriteRenderer>();
            if (spriteRendererComponent == null)
            {
                spriteRendererComponent = GetComponent<Image>();
                ObjectsDeformationManager.FadeInOut((Image) spriteRendererComponent, 1f, 0.15f);
            }
            else
            {
                ObjectsDeformationManager.FadeInOut((SpriteRenderer) spriteRendererComponent, 1f, 0.15f);
            }

            ObjectsDeformationManager.ScaleObject(scale, transform, 0.15f);
        }
        
        GetComponent<IClickItem>().tapOnItem();
    }
}
