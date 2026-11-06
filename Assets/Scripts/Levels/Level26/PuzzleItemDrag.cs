using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleItemDrag : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector3 mOffset;
    private Vector3 startPos;
    private float mZCoord;
    [SerializeField] private List<GameObject> target;
    [SerializeField] private float offset;
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        var pos = transform.position;
        var targ = target.OrderBy(obj => Vector3.Distance(obj.transform.position, pos)).First();
        if (targ != null)
        {
            if (Vector2.Distance(pos, targ.transform.position) <= offset)
            {
                GetComponent<IDraggableItems>().moveItem(targ.transform.position);
            }
            else
            {
                GetComponent<PuzzleItemController>().IsCorrectPos = false;
            }
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
