using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCircleToParent : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject triangle;
    [SerializeField] private GameObject[] otherObjects;
    [SerializeField] private SpriteRenderer[] allCircle;
    public bool isNeedToAffect = false;
    private EightLevelController _controller;
    private LevelContext _context;
    private Tween shaker;
    private Vector3 mOffset;
    private Vector3 startPos;
    private float mZCoord;
    private float scale;
    
    private void Awake()
    {
        _context = FindObjectOfType<LevelContext>();
        _controller = GetComponent<EightLevelController>();
        scale = transform.localScale.x;
        startPos = transform.position;
        shaker = ObjectsDeformationManager.Shake(transform).Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale, transform, 0.15f);
        var pos = transform.position;
        var freeParent = _context.PARENTS
            .FirstOrDefault(obj => Vector2.Distance(obj.transform.position, pos) <= offset);
        if (freeParent != null)
        {
            if (Vector2.Distance(freeParent.transform.position, transform.position) <= offset)
            {
                _controller.moveItem(freeParent.transform.position, freeParent);
                _context.removeElem(freeParent);
                if (_context.PARENTS.Count == 0)
                {
                    Invoke(nameof(specialEffect), 0.15f);
                }
            }
            else
            {
                transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear).OnComplete(() => shaker.Play());
            }
        }
        else
        {
            transform.DOMove(startPos, 0.15f).SetEase(Ease.Linear).OnComplete(() => shaker.Play());
        }
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        shaker.Pause();
        if(isNeedToAffect)
            ObjectsDeformationManager.ScaleObject(scale * 1.2f, transform, 0.15f);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void specialEffect()
    {
        triangle.SetActive(true);
        foreach (var spriteRenderer in allCircle)
        {
            spriteRenderer.DOFade(0, 0.05f).SetEase(Ease.Linear);
        }
        triangle.GetComponent<SpriteRenderer>().DOFade(1, 0.2f).OnComplete(() =>
        {
            triangle.GetComponent<SpriteRenderer>().DOFade(0.1f, 0.2f).OnComplete(() =>
            {
                triangle.GetComponent<SpriteRenderer>().DOFade(1, 0.2f).OnComplete(() =>
                {
                    triangle.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.35f).OnComplete(() =>
                    {
                        triangle.transform.DOScale(new Vector3(5f, 5f, 5f), 0.3f);
                        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
                    });
                });
            });
        });


    }
    
    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
