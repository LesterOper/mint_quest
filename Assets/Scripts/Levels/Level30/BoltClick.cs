using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using UnityEngine;

public class BoltClick : MonoBehaviour, IClickItem
{
    [SerializeField] private Animation _animation;
    [SerializeField] private PictureAccelerate _picture;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D pictureRig;
    [SerializeField] private Rigidbody2D bolt;
    [SerializeField] private GameObject first, second, thread;


    public void tapOnItem()
    {
        GetComponent<ClickController>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        
        _picture.enabled = false;
        
        thread.SetActive(false);
        second.SetActive(true);
        first.SetActive(true);
        pictureRig.simulated = true;
        bolt.bodyType = RigidbodyType2D.Dynamic;
        pictureRig.transform.DORotate(new Vector3(0, 0, 15f), 0.25f).SetEase(Ease.OutSine);
        Invoke(nameof(CompleteLevel), 0.3f);
    }

    private void CompleteLevel()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
    
    
    
}
