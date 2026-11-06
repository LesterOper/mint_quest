using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using DG.Tweening;

public class ToiletPaperController : MonoBehaviour
{
    [SerializeField] private GameObject _paper = null;
    [SerializeField] private GameObject _papersSheet = null;
    [SerializeField] private GameObject _papersSheetColider = null;
    [SerializeField] private GameObject _bin = null;
    [SerializeField] private GameObject _roll = null;
    [SerializeField] private List<SpriteRenderer> _renderers;
    
    private Vector3  _positionStart  = new Vector3(0.381f, 0.109f, 0f);
    private Vector3  _transformStart = new Vector3(0.95938350f, 0.9593835f, 1f);
    private Vector3 _positionEnd  = new Vector3(0.2391f, 0.0747f, 0f);
    private Vector3  _transformEnd= new Vector3(0.6112502f, 0.6277561f, 1f);
    private Vector3 _papersSheetStartPos  = new Vector3(0.075f, -0.118f, 0f);
    private Vector3  _papersSheetEndPos= new Vector3(0.241f, -0.118f, 0f);
    private ToiletPaperMover _MoveControllerPaper;
    public float currentSize = 0f;
    private bool isAvailable = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "restartPointForRoll")
        {
            if (isAvailable)
            {
                isAvailable = false;
                Sequence sequence = DOTween.Sequence();

                _renderers.ForEach(obj => { sequence.Join(obj.DOFade(0, 0.2f).SetEase(Ease.Linear)); });

                sequence.OnComplete(() => { FindObjectOfType<GameController>().RestartLevel(); });
            }
        }
    }

    private void RestartLVL()
    {
        _papersSheetColider.transform.position = new Vector3(0.15468f, -0.118f);
        _papersSheetColider.GetComponent<BoxCollider2D>().enabled = true;
        _papersSheetColider.GetComponent<ToiletPaperSheetController>().enabled = true;
        _MoveControllerPaper.ClearMoving();
        _roll.SetActive(false);
        transform.position = new Vector3(0.063f, 1.234f);
        SetPaperSizeInPercent(0f);
    }
    
    public void StartSize()
    {
        _paper.transform.localPosition = _positionStart;
        _paper.transform.localScale = _transformStart;
        _papersSheet.transform.localPosition = _papersSheetStartPos;
    }
    
        
    public void FinishSize()
    {
        _paper.transform.localPosition = _positionEnd;
        _paper.transform.localScale = _transformEnd;
        _papersSheet.transform.localPosition = _papersSheetEndPos;
    }

    public void MovePaperInBin()
    {
        SetPaperSizeInPercent(0f);
        _roll.gameObject.SetActive(false);
        _papersSheet.transform.DOMove(
            new Vector3(_papersSheet.transform.position.x+0.5f, _papersSheet.transform.position.y - 10f, 0f), 1f);
        _bin.transform.DOMove( new Vector3(0.07f, -4.23f), 1f).OnComplete(() => { _MoveControllerPaper.enabled = true;});
        
    }
    
    
    public float GetYPaperPosition()
    {
        return transform.position.y;
    }
    
    public void SetPaperSizeInPercent(float percent)
    {
        float offsetYpoxSheet = -10f+ ((-0.118f - -10f) / 100f) * percent;
        float offsetXposSheet = 0.241f - ((0.241f - 0.075f) / 100f) * percent;
        if (percent>=0f && percent<=100f)
        { 
            //Рулон
            float offsetXpos = 0.2391f + ((0.381f - 0.2391f) / 100f) * percent;
            float offsetYpox = 0.0747f +((0.109f- 0.0747f) / 100f) * percent;
            float transformX = 0.6112502f+ ((0.95938350f - 0.6112502f) / 100f) * percent;
            float transformY = 0.6277561f+((0.9593835f - 0.6277561f) / 100f) * percent;
            _paper.transform.localPosition = new Vector3(offsetXpos, offsetYpox, 0f);
            _paper.transform.localScale = new Vector3(transformX, transformY, 0f); 
            //Свисающая бумажка
        }
        //Свисающая бумажка
        _papersSheet.transform.localPosition = new Vector3(offsetXposSheet, offsetYpoxSheet, 0f);
    }
    
    [Button]
    public void IncreaseSize()
    {
        currentSize += 1f;
        SetPaperSizeInPercent(currentSize);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _MoveControllerPaper = GetComponent<ToiletPaperMover>();
        _MoveControllerPaper.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
