using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ButtonsOrderHelper : MonoBehaviour
{
   [SerializeField] private List<GameObject> actualOrder;
   [SerializeField] private GameObject fill;
   [SerializeField] private List<SpriteRenderer> arrows;
   private List<GameObject> currentOrder;
   private GameObject currentButton;
   private int index = 0;
   private float startY, incrementPosY;
   private float increment = 0.0675f;
   private void Start()
   {
      currentOrder = new List<GameObject>(actualOrder);
      currentButton = currentOrder[0];
      startY = fill.transform.position.y;
      incrementPosY = startY;
   }

   public void CheckOrder(GameObject obj)
   {
      if (obj == currentButton)
      {
         incrementPosY += increment;
         currentOrder.RemoveAt(0);
         arrows[index].DOFade(0.4f, 0.15f).SetEase(Ease.Linear).OnComplete(() => { arrows[index].DOKill(); });
         fill.transform.DOMoveY(incrementPosY, 0.15f).SetEase(Ease.Linear).OnComplete(() => { fill.transform.DOKill(); });
         if (currentOrder.Count == 0)
         {
            fill.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            fill.transform.DOScale(new Vector3(4, 4, 4), 0.2f).SetEase(Ease.Linear);
            Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
            return;
         }

         currentButton = currentOrder[0];
         index++;
      }
      else
      {
         Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

         ResetButtons();
      }
   }

   private void ResetButtons()
   {
      currentOrder = new List<GameObject>(actualOrder);
      currentButton = currentOrder[0];
      arrows.ForEach(obj => { obj.DOFade(1, 0.1f).SetEase(Ease.Linear).OnComplete(() => { obj.DOKill(); }); });
      fill.transform.DOMoveY(startY, 0.15f).SetEase(Ease.Linear);
      incrementPosY = startY;
      index = 0;
   }
}
