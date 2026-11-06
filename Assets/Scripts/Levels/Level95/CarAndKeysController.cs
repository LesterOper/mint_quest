using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using UnityEngine;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;

public class CarAndKeysController : MonoBehaviour
{
    [SerializeField] private GameObject _wheel = null;
    [SerializeField] private GameObject _wheelDisk = null;
    [SerializeField] private GameObject _key1 = null;
    [SerializeField] private GameObject _key2 = null;
    [SerializeField] private GameObject _key3 = null;
    [SerializeField] private GameObject _key4 = null;
    [SerializeField] private GameObject _objWheelPosition = null;
    [SerializeField] private GameObject _car = null;
    [SerializeField] private GameObject _mintBckgr = null;
    private Camera mainCamera;
    private Vector3 _wheelPosition;
    private CarStatus _rideStatus = CarStatus.NOTHING;
    private bool isTrackingCameraCar;
    
    public enum CarStatus
    {
        NOTHING = 0,
        WHEEL_WAS_SET = 1,
        DISK_WAS_SET = 2,
        WALL_WAS_BROCKEN = 3,
        KEY_WAS_SET = 4,
    }

    private void OnDestroy()
    {
        DOTween.Kill(_wheel.transform);
        DOTween.Kill(_key1.transform);
        DOTween.Kill(_wheelDisk.transform);
        DOTween.Kill(mainCamera.transform);
    }

    private void OnDisable()
    {
        mainCamera.orthographicSize = 7f;
      //  
    }

    public CarStatus GetRideStatus()
    {
        return _rideStatus;
    }

    private void SetRideStatus(CarStatus _CarStatus)
    {
        _rideStatus = _CarStatus;
        if (_CarStatus == CarStatus.WHEEL_WAS_SET)
        {
            _wheelDisk.GetComponent<MoveController>().enabled = true;
        }
        if (_CarStatus == CarStatus.WALL_WAS_BROCKEN)
        {
            Debug.Log("Activate Key");
            _key1.GetComponent<MoveController>().enabled = true;
            _key2.GetComponent<MoveController>().enabled = true;
            _key3.GetComponent<MoveController>().enabled = true;
            _key4.GetComponent<MoveController>().enabled = true;
        }
    }
    
    public void SetWheelOnPlace()
    {
        _wheel.GetComponent<MoveController>().enabled = false;
        _wheel.transform.DOMove(_wheelPosition, 0.3f).SetEase(Ease.Flash).OnComplete(() => SetRideStatus(CarStatus.WHEEL_WAS_SET));
    }
    
    
    public void SetDiskOnPlace()
    {
        _wheelDisk.GetComponent<MoveController>().enabled = false;
        _wheelDisk.transform.DOMove(_wheelPosition, 0.3f).SetEase(Ease.Flash).OnComplete(() => SetRideStatus(CarStatus.DISK_WAS_SET));
    }

    public void DestroyWall()
    {
        Debug.Log("Стена пала");
        SetRideStatus(CarStatus.WALL_WAS_BROCKEN);
    }

    public void DestroyKey()
    {
        DOTween.Kill(_key1.transform);
        _key1.gameObject.SetActive(false);
        _car.GetComponent<Animation>().Play();
        mainCamera.transform.DOMoveX(mainCamera.transform.position.x - 22f, 1.2f).OnComplete(ShowCoolBtn);
    }

    private void ShowCoolBtn()
    {
        mainCamera.orthographicSize = 7f;
        mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        _mintBckgr.transform.localScale = new Vector3(50f,50f,50f);
        isTrackingCameraCar = false;
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
    
    public void OpenDoor()
    {
        _rideStatus = CarStatus.KEY_WAS_SET;
        _key4.GetComponent<MoveController>().enabled = false;
        //Context.SoundPlayer.PlaySoundClick("car_open_signal", AudioMixerController.MasterMixerGroup.SFX, 1);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_key1.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.40f).SetEase(Ease.Linear));
        sequence.Join(_key1.transform.DORotate(new Vector3(1f, 1f, 90f), 0.40f).SetEase(Ease.Linear));
        //sequence.Join(_key1.transform.DOLocalMove(new Vector3(0.78f, -0.276f), 0.40f).SetEase(Ease.Linear));
        sequence.Join(_key1.transform.DOLocalMove(new Vector3(0.091f, 0.97f), 0.40f).SetEase(Ease.Linear));
        sequence.Append(_key1.transform.DOLocalMove(new Vector3(0.145f, 0.99f, 0.3f), 0.25f).SetEase(Ease.Linear));
        sequence.Append(_key1.transform.DOScaleX(-0.3f, 0.25f).SetEase(Ease.Linear));
        sequence.Append(_key1.transform.DOScaleX(0.3f, 0.25f).SetEase(Ease.Linear));        
        sequence.Append(_key1.transform.DOScaleX(-0.3f, 0.25f).SetEase(Ease.Linear)).OnComplete(DestroyKey);

     

    }
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = 5.3f;
        isTrackingCameraCar = true;
        _wheelPosition = _objWheelPosition.transform.position;
        _wheelDisk.GetComponent<MoveController>().enabled = false;
        _key1.GetComponent<MoveController>().enabled = false;
        _key2.GetComponent<MoveController>().enabled = false;
        _key3.GetComponent<MoveController>().enabled = false;
        _key4.GetComponent<MoveController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrackingCameraCar)
        {
            //        mainCamera.transform.position = new Vector3(_car.transform.position.x-0.4f, mainCamera.transform.position.y, -10f);
        }
        else
        {
          //  mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        }
    }
}
