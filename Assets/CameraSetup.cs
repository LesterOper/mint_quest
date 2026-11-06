using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
   private void Start()
   {
      GetComponentInParent<Canvas>().worldCamera = Camera.main;
   }
}
