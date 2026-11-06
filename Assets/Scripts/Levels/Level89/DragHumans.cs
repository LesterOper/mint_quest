using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHumans : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject target;
    [SerializeField] private float offset;
    private GameObject parent;
    public bool isDrag;
    private bool isDragExitLevel;

    private Vector3 mOffset;
    private Vector3 startPos;
    private float mZCoord;

    private void Start()
    {
        startPos = transform.position;
        parent = FindObjectOfType<RollingHelper>().gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
        isDragExitLevel = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        var pos = transform.position;
        if (target != null)
        {
            if (Vector2.Distance(pos, target.transform.position) <= offset)
            {
                GetComponent<IDraggableItems>().moveItem(target.transform.position);
            }
            else
            {
                GetComponent<SpriteRenderer>().DOFade(0, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().DOFade(0, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.SetParent(parent.transform);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isDrag = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        //x,y
        Vector3 mousePoint = Input.mousePosition;
        //z
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public bool getDrag()
    {
        return isDrag;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
    

