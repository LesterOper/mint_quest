using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class AccelarateBallWay : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject boost;
    [SerializeField] private EdgeCollider2D closedWayOne, closedWayTwo;
    [SerializeField] private EdgeCollider2D main, insideColl;
    [SerializeField] private BoxCollider2D firstDoor, secondDoor;
    private Vector2 dir;
    private float speed = 3f;
    private bool isEnable = true;
    private bool isFixedEnable = true;
    void Update()
    {
        if(!isEnable) return;
        Vector2 acc = Input.acceleration;
        dir = acc * speed;
    }

    private void FixedUpdate()
    {
        if(!isFixedEnable) return;
        if (transform.position.y <= -2.8f)
        {
            isFixedEnable = false;
            main.enabled = false;
            firstDoor.enabled = false;
            secondDoor.enabled = false;
            insideColl.enabled = false;
            Camera.main.transform.DOMoveY(-6.3f, 0.5f).SetEase(Ease.Linear);
            transform.DOMove(new Vector2(transform.position.x,-6.3f), 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            });
        }
        else
        {
            if(!isEnable) rb.velocity = new Vector2(0, -1 * speed);
            else rb.velocity = new Vector2(dir.x, dir.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == boost)
        {
            Context.SoundPlayer.PlaySoundClick("right_do");
            Debug.Log("BOOST");
            closedWayOne.enabled = false;
            closedWayTwo.enabled = false;
            firstDoor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            secondDoor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            speed = 8f;
            rb.AddForce(Vector2.down * speed, ForceMode2D.Force);
            isEnable = false;
        }
    }
}
