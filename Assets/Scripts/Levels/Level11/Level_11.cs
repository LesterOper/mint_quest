using System.Collections;
using System.Collections.Generic;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class Level_11 : MonoBehaviour, IClickItem
{
    [SerializeField] private Sprite active;
    [SerializeField] private Sprite deActive;
    [SerializeField] private string name;
    [SerializeField] private int countCorners;
    [SerializeField] private Level_11_SupportClass _support;
    [SerializeField] private GameObject spriteActive;
    public int COUNT => countCorners;
    public void tapOnItem()
    {
        if (_support.isCorrect(gameObject))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            //GetComponent<SpriteRenderer>().sprite = active;
            spriteActive.SetActive(true);
            _support.addToCurrentActive(gameObject);
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error");
            _support.DeActivateAllObjects();
        }
    }

    public void ActivateForEffect()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().sprite = active;
        //spriteActive.SetActive(true);
        Invoke(nameof(ActivateInvoke), 0.2f);
    }
    
    public void DeActive()
    {
        //GetComponent<SpriteRenderer>().sprite = deActive;
        //spriteActive.SetActive(false);
        Invoke(nameof(DeactivateInvoke), 0.2f);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void ActivateInvoke()
    {
        spriteActive.SetActive(true);
    }

    private void DeactivateInvoke()
    {
        spriteActive.SetActive(false);
    }
}
