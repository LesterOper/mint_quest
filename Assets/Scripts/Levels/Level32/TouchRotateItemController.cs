using System;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;


public class TouchRotateItemController : MonoBehaviour, IClickItem
{
    [SerializeField] private SwipeLevel _touch;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private bool isClick = false;
    private bool isEnable = true;
    private float timeToNextRotate = 0f;
    private Vector3 startPos;

    private CircleCollider2D coll;

    private void Start()
    {
        startPos = transform.position;
        coll = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (!isEnable) return;
        timeToNextRotate -= Time.deltaTime;
        if (!isClick)
        {
            if (timeToNextRotate <= 0)
            {
                transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + 90f), speed);
                timeToNextRotate = 1f;
            }
        }
        else 
            transform.Translate(Vector2.left * 3f * Time.deltaTime);
        if (Math.Abs(transform.position.x) >= 7f || Math.Abs(transform.position.y) >= 7f)
        {
            _touch.Increment();
            coll.enabled = false;
            isEnable = false;
        }
    }

    public void tapOnItem()
    {
        isClick = true;
        float angle = rb.rotation * Mathf.Deg2Rad;
        Debug.Log(angle);
        _touch.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(startPos, 0.25f).SetEase(Ease.Linear);
        isClick = false;
    }
}
