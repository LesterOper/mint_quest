using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class BoxIqosContr : MonoBehaviour, IClickItem
{
    [SerializeField] private Sprite openState;
    [SerializeField] private Sprite closeState;
    [SerializeField] private GameObject target;
    private bool _isOpen;
    public void tapOnItem()
    {
        _isOpen = !_isOpen;
        GetComponent<SpriteRenderer>().sprite =_isOpen ? openState : closeState;
        //FindObjectOfType<SupportLevelScript>().setAttachForIqos();
        target.SetActive(_isOpen);
        //GetComponent<BoxCollider2D>().enabled = false;
    }
    
    public void changeState()
    {
        GetComponent<SpriteRenderer>().sprite = closeState;
        _isOpen = false;
    }

    public bool getIsOpen()
    {
        return _isOpen;
    }
}
