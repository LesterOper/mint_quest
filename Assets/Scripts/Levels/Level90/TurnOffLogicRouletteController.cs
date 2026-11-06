using System;
using App.Global;
using UnityEngine;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;

public class TurnOffLogicRouletteController : MonoBehaviour
{
    [SerializeField] private GameObject _objNumber7 = null;
    [SerializeField] private GameObject _objRoulettePointer = null;
    [SerializeField] private TurnOffLogicPointer _scrTurnOffLogicPointer = null;
    [SerializeField] private GameObject _objTurnOffLogicPointer = null;

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(_objNumber7.transform);
        DOTween.Kill(_objRoulettePointer.transform);
    }

    public void StartEffect()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("_objRoulettePointer.transform.rotation.z " +_objRoulettePointer.transform.rotation.z);
        _scrTurnOffLogicPointer.enabled = false;
        RotateToCenter();
    }
    
    private void RotateToCenter()
    {
        transform.DORotate(new Vector3(0f, 0f, (360f-_objRoulettePointer.transform.rotation.eulerAngles.z)), 0.8f).SetEase(Ease.Linear).OnComplete(HitNumber7);
    }

    private void HitNumber7()
    {
        Context.SoundPlayer.PlaySoundClick("ts3");
        Sequence sequence1 = DOTween.Sequence();
        sequence1.AppendInterval(0.2f);
        sequence1.Append(_objNumber7.transform.DOJump(new Vector3(_objNumber7.transform.position.x, _objNumber7.transform.position.y, 0f), 0.3f, 3, 0.50f)).OnComplete(ShowMintBackground);
        _objRoulettePointer.transform.DOJump(new Vector3(transform.position.x, transform.position.y , 0f), 0.8f,1,0.75f);
    }

    private void ShowMintBackground()
    {
        _objTurnOffLogicPointer.transform.DOScale(new Vector3(5f, 5f, 5f), 1.8f);
        ShowCoolBtn();
    }

    private void ShowCoolBtn()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}
