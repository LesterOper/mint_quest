using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnOffLogicPointer : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TurnOffLogicRouletteController _controller;
    private bool isDrag;
    
    public void OnDrag(PointerEventData eventData)
    { 
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0); 
      Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
      float Angle = Mathf.Atan2(worldPos.y - transform.position.y, worldPos.x - transform.position.x) / Mathf.PI * 180f;
      transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y , Angle-90f);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        var pos = transform.position;

        Debug.Log(transform.rotation.z * 100);
        if (transform.rotation.z * 100 > -97f && transform.rotation.z * 100 < -86f)
        {
            Context.SoundPlayer.PlaySoundClick("right_do");
            _controller.StartEffect();
        }
        isDrag = false;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        isDrag = true;
    }
}
