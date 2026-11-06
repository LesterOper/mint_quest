using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{
    [SerializeField] private List<GameObject> coins;
    [SerializeField] private List<GameObject> correctCoins;
    [SerializeField] private Vector2[] startPos;
    private List<GameObject> coinsInCollector = new List<GameObject>();
    private int count;
    //private int i = 0;
    
    public void CheckCoins(GameObject obj)
    {
        coinsInCollector.Add(obj);
        obj.GetComponent<SpriteRenderer>().DOFade(0, 0.15f).SetEase(Ease.Linear);
        if (correctCoins.Contains(obj) && coinsInCollector.Count <= 2)
        {
            count++;
            if (count == 2)
            {
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            }
        }
    }

    public void RefreshCoins()
    {
        int i = 0;
        for (i = 0; i < coins.Count; i++)
        {
            coins[i].GetComponent<CircleCollider2D>().enabled = true;
            coins[i].transform.DOMove(coins[i].GetComponent<DragController>().GetStartPos(), 0.15f).SetEase(Ease.Linear).OnComplete(() => coins[i].GetComponent<DragController>().Shaker.Play());
            coins[i].GetComponent<SpriteRenderer>().DOFade(1, 0.15f).SetEase(Ease.Linear);
        }
        /*coins.ForEach(obj =>
        {
            obj.GetComponent<CircleCollider2D>().enabled = true;
            obj.transform.DOMove(startPos[i], 0.15f).SetEase(Ease.Linear);
            i++;
        });*/
        coinsInCollector.Clear();
        count = 0;

        //correctCoins.Clear();
    }
}
