using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchboxController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private MatchAndFireController _matchAndFireController = null;
    private bool isDrag;
    private Vector3 mOffset;
    private float mZCoord;

    public void OnDrag(PointerEventData eventData)
    {
            transform.position = GetMouseWorldPos() + mOffset;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _matchAndFireController.ShowBtnRotate();
            isDrag = false;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        _matchAndFireController.HideBtnRotate();
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
}
