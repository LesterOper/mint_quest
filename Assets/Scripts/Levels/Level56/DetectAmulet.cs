using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAmulet : MonoBehaviour
{
   [SerializeField] private AmuletBallController _controller;
   [SerializeField] private Transform target;
   private void OnCollisionEnter2D(Collision2D other)
   {
      _controller.ResetBall();
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.name == target.gameObject.name)
      {
         _controller.moveItem(target.position);
      }
   }
}
