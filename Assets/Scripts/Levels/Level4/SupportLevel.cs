using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SupportLevel : MonoBehaviour
{
    [SerializeField] private GameObject pieceOfCircle;
    [SerializeField] private GameObject otherPiece;
    private string nameOther;
    private bool isPeaceRotate = false;
    private bool isOtherRotate = false;

    private void Start()
    {
        pieceOfCircle.GetComponent<DragController>().enabled = false;
    }

    public void setEnableDragPiece()
    {
        pieceOfCircle.GetComponent<DragController>().enabled = true;
        pieceOfCircle.GetComponent<RotateController>().enabled = false;
        otherPiece.GetComponent<RotateController>().enabled = false;
    }

    public void SetIsRotate(bool isPeice)
    {
        if (isPeice)
        {
            isPeaceRotate = true;
        }
        else
        {
            isOtherRotate = true;
        }
        CheckIsEnableToDrag();
    }

    private void CheckIsEnableToDrag()
    {
        if (isOtherRotate && isPeaceRotate)
        {
            pieceOfCircle.GetComponent<DragController>().enabled = true;
        }
    }
}
