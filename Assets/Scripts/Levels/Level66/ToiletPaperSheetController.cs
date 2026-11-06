using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToiletPaperSheetController : MonoBehaviour
{
    [SerializeField] private ToiletPaperController _toiletPaperController = null;

    private float percentPaper = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        percentPaper = 100f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Зашло!!!!!!!!");
        GetComponent<MoveXYController>().enabled = false;
        GetComponent<ToiletPaperSheetController>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        _toiletPaperController.MovePaperInBin();
        // Бокс коллайдер и
        // мув коллайдер у бумаги и боксколлайдера
    }

    public float GetPercentPaper()
    {
        percentPaper = (100f -(transform.localPosition.y / -0.09882f));
        return percentPaper;
    }

    // Update is called once per frame
    void Update()
    {
        GetPercentPaper();
        if (percentPaper>100f)
        {
            percentPaper = 99.999f;
            _toiletPaperController.SetPaperSizeInPercent(99.999f);
            transform.position = new Vector3(transform.position.x, _toiletPaperController.GetYPaperPosition(), 0f);
        }

        _toiletPaperController.SetPaperSizeInPercent(GetPercentPaper());
    }
}
