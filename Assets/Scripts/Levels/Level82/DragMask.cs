using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMask : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject fill;
    [SerializeField] private Animation _animation;
    [SerializeField] private GameObject fillCircle;
    private Vector3 mOffset;
    private float mZCoord;
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Vector2.Distance(transform.position, person.transform.position) >= 0.7f)
        {
            GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear);
            fill.transform.DOMoveY(-0.9f, 0.6f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _animation.Play();
                Context.SoundPlayer.PlaySoundClick("caugh_3", AudioMixerController.MasterMixerGroup.SFX, 1);
                Invoke(nameof(Complete), 1.5f);
            });
        }
    }

    private void Complete()
    {
        fillCircle.SetActive(true);
        fillCircle.transform.DOScale(new Vector3(4f, 4f, 4f), 0.4f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
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
