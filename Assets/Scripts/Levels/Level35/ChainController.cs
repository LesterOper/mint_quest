using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ChainController : MonoBehaviour
{
    private int countChainDismissElem = 0;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private List<GameObject> chainElems;
    private int index = 0;

    public bool IncrementCountAndCheckChain(GameObject el)
    {
        if (chainElems[index] == el)
        {
            countChainDismissElem++;
            index++;
            if (countChainDismissElem == 6)
            {
                SpecialEffect();
            }
            
            return true;
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error");
            ResetChain();
            return false;
        }
        
    }

    private void SpecialEffect()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(sprites[0].DOFade(0.3f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[2].DOFade(0.3f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[4].DOFade(0.3f, 0.3f).SetEase(Ease.Linear));
        
        sequence.Append(sprites[1].DOFade(0.3f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[3].DOFade(0.3f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[5].DOFade(0.3f, 0.3f).SetEase(Ease.Linear));
        
        sequence.Append(sprites[0].DOFade(1f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[2].DOFade(1f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[4].DOFade(1f, 0.3f).SetEase(Ease.Linear));
        
        sequence.Append(sprites[1].DOFade(1f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[3].DOFade(1f, 0.3f).SetEase(Ease.Linear));
        sequence.Join(sprites[5].DOFade(1f, 0.3f).SetEase(Ease.Linear));

        sequence.OnComplete(() =>
        {
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        });
    }

    public void ResetChain()
    {
        chainElems[0].transform.DOMoveX(0f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                chainElems[0].GetComponent<BoxCollider2D>().enabled = true;
            });
        chainElems[1].transform.DOScale(new Vector3(0.65f, 0.7f, 0.8f), 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            chainElems[1].GetComponent<BoxCollider2D>().enabled = true;
        });
        chainElems[2].transform.DOMoveX(0f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            chainElems[2].GetComponent<BoxCollider2D>().enabled = true;
        });
        chainElems[3].transform.DOScale(new Vector3(0.65f, 0.7f, 0.8f), 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            chainElems[3].GetComponent<BoxCollider2D>().enabled = true;
        });
        chainElems[4].transform.DOScale(new Vector3(0.65f, 0.7f, 0.8f), 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            chainElems[4].GetComponent<BoxCollider2D>().enabled = true;
        });
        chainElems[5].transform.DOMoveX(0f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            chainElems[5].GetComponent<BoxCollider2D>().enabled = true;
        });
        index = 0;
        countChainDismissElem = 0;
    }
}
