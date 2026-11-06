using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class WindowClicker : MonoBehaviour, IClickItem
{
    [SerializeField] private WindowOrderer _windowOrderer;
    [SerializeField] private SpriteRenderer thisWindow;
    [SerializeField] private Sprite active, deActive;
    public void tapOnItem()
    {
        if (_windowOrderer.Check(thisWindow))
        {
            ActivateWindow();
        }
    }

    private void ActivateWindow()
    {
        thisWindow.sprite = active;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    public void ChangeSprite()
    {
        thisWindow.sprite = deActive;
        GetComponent<PolygonCollider2D>().enabled = true;
    }
}
