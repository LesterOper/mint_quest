using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class DetectCollisionWithBals : MonoBehaviour
{
   [SerializeField] private CircleCollider2D correctBall, incorrectBall;
   [SerializeField] private List<Collider2D> colliders;
   [SerializeField] private Rigidbody2D fake;
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.collider == correctBall)
      {
         
         fake.simulated = false;
         correctBall.enabled = false;
         colliders.ForEach(obj =>
         {
            obj.enabled = false;
         });
         Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
      }
      else
      {
         FindObjectOfType<GameController>().RestartLevel();
      }
      Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
      correctBall.GetComponent<AccelearateBalls>().enabled = false;
      incorrectBall.GetComponent<AccelearateBalls>().enabled = false;
   }
   
   
}
