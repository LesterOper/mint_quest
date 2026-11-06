using System;
using App.Global;
using UnityEngine;
using UnityEngine.U2D;
using DG.Tweening;
using Swimmer.Library;

public class pawlController : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _zipperRight = null;
    [SerializeField] private SpriteShapeController _zipperLeft = null;
    [SerializeField] private GameObject _mintBackground = null;
    private Vector3 _pawlStartPosition;
    private MoveXYController _moveXyController;
    private bool isEnd = false;
    private bool isMoveToStart = false;
    private bool isEnable = true;

    private void OnDestroy()
    {
        DOTween.Kill(_zipperRight.transform);
        DOTween.Kill(_zipperLeft.transform);
        DOTween.Kill(transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isEnd = true;
        _mintBackground.transform.localScale = new Vector3(5.63f, 50f);
        ShowCoolBtn();
        isEnable = false;
        GetComponent<MoveXYController>().enabled = false;
        transform.DOMoveY(transform.position.y - 5, 1f);
        _zipperRight.gameObject.transform.DOMove(
            new Vector3(_zipperRight.gameObject.transform.position.x + 4f,
                _zipperRight.gameObject.transform.position.y + 2f), 1f);
        _zipperLeft.gameObject.transform.DOMove(
            new Vector3(_zipperLeft.gameObject.transform.position.x - 4f,
                _zipperLeft.gameObject.transform.position.y + 2f), 1f);
    }

    private void ShowCoolBtn()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }

    private void Start()
    {
        _pawlStartPosition = new Vector3(0f, 5.04f, 0f);
        _moveXyController = GetComponent<MoveXYController>();
    }

    private void StopMoveToStart()
    {
        isMoveToStart = false;
        DOTween.Kill(transform);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isEnable) return;
        Debug.Log("RUN");
        _mintBackground.transform.position = new Vector3(0f, transform.position.y+28f,0f);
        _zipperRight.spline.SetPosition( 1,new Vector3( 0f, transform.position.y-1.3f, 0f));
        _zipperLeft.spline.SetPosition( 1,new Vector3( 0f, transform.position.y-1.3f, 0f));
        
        if (_moveXyController.GetDragStatus() && !isEnd)
        {
            StopMoveToStart();
          
        }
        else
        {
            if (!isMoveToStart && !isEnd)
            {
                isMoveToStart = true;
                transform.DOMoveY(_pawlStartPosition.y, 0.8f);
            }
        }


    }
}
