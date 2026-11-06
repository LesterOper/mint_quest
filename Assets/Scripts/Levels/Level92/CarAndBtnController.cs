using System;
using App;
using App.Global;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Swimmer.Library.Common;

public class CarAndBtnController : MonoBehaviour, IPointerClickHandler
{
      [SerializeField] private GameObject _car = null;
      [SerializeField] private GameObject _mintBackground = null;
    
      private float _AccelerateY;
      float smoothTime = 0.35f;
      float yVelocity = 0.0f;

      private float newPosition = 0f;

      private float startPosition = -2.19f;
      private float endPosition = 1.19f;
      private float currentPosition = 1.19f;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = startPosition;
    }

    private void OnDestroy()
    {
        CancelInvoke("ShowCoolBtn");
        DOTween.Kill(transform);
        DOTween.Kill(_mintBackground.transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        if (_car.transform.position.y>(endPosition*0.97))
        {
            _mintBackground.transform.DOScale(new Vector3(6f, 6f, 6f), 2f);
            Invoke("ShowCoolBtn", 0.5f);
        }

    }

    public void RefreshAccelerationData()
    {
        _AccelerateY = Input.acceleration.y;
    }
    
    private void ShowCoolBtn()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }

    public void RefreshCarPosition()
    {        
             RefreshAccelerationData();
            if (_AccelerateY<0)
            {
                currentPosition = startPosition;
            }
            else
            {
                currentPosition = endPosition;
            }
            newPosition = Mathf.SmoothDamp(_car.transform.position.y, currentPosition, ref yVelocity, smoothTime);
    }
    
    void Update()
    {
        RefreshCarPosition();
        _car.transform.position = new Vector3(_car.transform.position.x, newPosition, _car.transform.position.z);
    }
}
