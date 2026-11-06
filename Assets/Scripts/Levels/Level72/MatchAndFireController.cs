using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class MatchAndFireController : MonoBehaviour
{
    [SerializeField] private GameObject _btnRotate = null;
    [SerializeField] private GameObject _rotatePlace = null;
    [SerializeField] private GameObject _matchBox= null;
    [SerializeField] private BoxCollider2D _brimstone = null;
    [SerializeField] private GameObject _fire = null;
    
    [SerializeField] public Transform transformMatchBox;
    [SerializeField] public Transform transformWorld;
    [SerializeField] private GameObject mask;
    [SerializeField] private MatchPhysicsController match;

    public void ActivateFire()
    {
        _fire.SetActive(true);
        _fire.transform.DOLocalMoveY(_fire.transform.position.y - 2f, 3.5f).SetEase(Ease.Linear);
        mask.transform.DOLocalMoveY(0.5f, 3f).SetEase(Ease.Linear).OnComplete(() => { match.ActivatePhysics(); });
    }
    
    public void RotateMatchbox()
    {
        _btnRotate.transform.SetParent(transformMatchBox);
        _matchBox.GetComponent<MatchboxController>().enabled = false;
        _matchBox.transform.DORotate(new Vector3(_matchBox.transform.eulerAngles.x, _matchBox.transform.eulerAngles.y, _matchBox.transform.eulerAngles.z +90f), 0.3f).OnComplete(ReturnWorldsParent);
        
        if (_matchBox.transform.eulerAngles.z >= 89f && _matchBox.transform.eulerAngles.z <= 91f)
        {
            Debug.Log("123124");
        }
    }

    public void ReturnWorldsParent()
    {
        _matchBox.GetComponent<MatchboxController>().enabled = true;
        transform.SetParent(transformWorld);
    }
    
    public void HideBtnRotate()
    {
        _btnRotate.SetActive(false);
    }
    
    public void ShowBtnRotate()
    {
        _btnRotate.SetActive(true);
        SetPlaceBtnRotate();
    }

    public void SetPlaceBtnRotate()
    {
        _btnRotate.transform.position = _rotatePlace.transform.position;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
