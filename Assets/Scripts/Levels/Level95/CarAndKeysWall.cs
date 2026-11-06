using System;
using System.Collections;
using System.Collections.Generic;
using App;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Swimmer.Library.Common;

public class CarAndKeysWall : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]  private CarAndKeysController _scrCarAndKeysController = null;
    private int WallsHP = 5;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
         if (_scrCarAndKeysController.GetRideStatus() == CarAndKeysController.CarStatus.DISK_WAS_SET )
         {
             WallsHP--;
             
             if (WallsHP==0)
             {
                 transform.DOJump(new Vector3( transform.position.x, transform.position.y - 6f), 3.5f, 1, 0.6f);
                 _scrCarAndKeysController.DestroyWall();
             }
             else
             {
                 transform.DOJump(new Vector3( transform.position.x, transform.position.y - 1f), 1.2f, 1, 0.3f);

             }
         }
    }

}
