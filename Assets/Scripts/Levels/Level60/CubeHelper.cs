using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class CubeHelper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer back;
    [SerializeField] private CubePieceController[] pieces;
    private int count = 0;
    
    public void IncrementConnectedPieces()
    {
        count++;
        if (count == 5)
        {
            Effect();
        }
    }

    private void Effect()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].ChangeSprite();
        }

        back.DOFade(1, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Invoke(nameof(AddToEffect), 0.4f);
        });
        
    }

    private void AddToEffect()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].FadeSprite();
        }
        Invoke(nameof(Complete), 1f);
    }

    private void Complete()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}
