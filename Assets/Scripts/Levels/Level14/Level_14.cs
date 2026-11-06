using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class Level_14 : MonoBehaviour, IClickItem
{
    [SerializeField] private int needCountClicks;
    [SerializeField] private LevelHelper _levelHelper;
    [SerializeField] private GameObject foreground;
    [SerializeField] private string name;
    private int countClicks = 0;
    private bool isClickedAllTimes = false;
    private bool isClosed = false;
    private const float diff = 1.65f;

    public void tapOnItem()
    {
        GetComponent<SpriteRenderer>().DOFade(0.4f, 0.3f).OnComplete(() =>
        {
            GetComponent<SpriteRenderer>().DOFade(1f, 0.4f);
        });
        if (_levelHelper.getCurrentNumber().name == name)
        {
            countClicks += 1;
            if (countClicks == needCountClicks)
            {
                isClickedAllTimes = true;
                GetComponent<CircleCollider2D>().enabled = false;
                closeForeground();
                _levelHelper.detectedCorrectTaps(this);
            }
        }
        else
        {
            _levelHelper.ResetButtons();
        }
    }

    public void openForeground()
    {
        if (isClosed)
        {
            foreground.transform
                .DOMove(new Vector3(foreground.transform.position.x - diff, foreground.transform.position.y), 0.4f);
            GetComponent<CircleCollider2D>().enabled = true;
            isClosed = false;
        }
        countClicks = 0;
    }

    private void closeForeground()
    {
        if (!isClosed)
        {
            foreground.transform.DOMove(
                new Vector3(foreground.transform.position.x + diff, foreground.transform.position.y), 0.4f);
            isClosed = true;
        }
    }
}
