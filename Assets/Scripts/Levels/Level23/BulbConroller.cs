using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using TMPro;
using UnityEngine;

public class BulbConroller : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject bulb;
    [SerializeField] private Sprite activeBulb;
    [SerializeField] private TextMeshProUGUI mess;
    [SerializeField] private GameObject mint;
    public void tapOnItem()
    {
        GetComponent<SpriteRenderer>().DOFade(0.3f, 0.15f).OnComplete(() =>
        {
            GetComponent<SpriteRenderer>().DOFade(1, 0.15f);
            bulb.GetComponent<SpriteRenderer>().sprite = activeBulb;
        });
        
        //mess.transform.GetComponent<RectTransform>().DOAnchorPosY(400f, 1f).SetEase(Ease.OutBounce);
        mess.DOFade(0, 0.8f).SetEase(Ease.Linear).OnComplete(() =>
            {
                mint.transform.DOScale(new Vector3(4, 4, 4), 0.3f);
            });
        Invoke(nameof(CompleteEffect), 1.1f);
    }

    private void CompleteEffect()
    {
        mess.gameObject.SetActive(false);
        FindObjectOfType<SwipeScreen>().gameObject.SetActive(false);
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}
