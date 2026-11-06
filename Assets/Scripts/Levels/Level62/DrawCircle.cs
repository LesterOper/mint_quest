using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    [SerializeField] private CircleCollider2D click;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject targetPosObject;
    private LineRenderer _lineRenderer;
    public GameObject targ;
    public GameObject linePrefab;
    private List<Vector2> positions = new List<Vector2>();
    private GameObject currentLine;
    private bool isEnableDraw = true;
    private Vector2 startPosOfTarget;
    private CircleClick _click;
    

    private void Start()
    {
        _click = FindObjectOfType<CircleClick>();
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        startPosOfTarget = targetPosObject.transform.position;
        MoveByCircle();
    }

    private void Update()
    {
        if(!isEnableDraw) return;
        Vector3 angleGyro = Input.gyro.attitude.eulerAngles;

        if (angleGyro.z <= 90)
        {
            transform.RotateAround(targ.transform.position, Vector3.forward, 60f * Time.deltaTime);
            Vector2 pos = transform.position;
            if (Vector2.Distance(pos, positions[positions.Count - 1]) > .02f)
            {
                UpdateLine(pos);
            }
        }

        if (Vector2.Distance(startPosOfTarget, transform.position) <= .05f)
        {
            Context.SoundPlayer.PlaySoundClick("right_do");
            _click.enabled = true;
            isEnableDraw = false;
            click.enabled = true;
        }
        
    }

    private void MoveByCircle()
    {
        CreateLine();
    }


    private void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLine.transform.SetParent(parent.transform);
        _lineRenderer = currentLine.GetComponent<LineRenderer>();
        positions.Add(transform.position);
        positions.Add(transform.position);
        _lineRenderer.SetPosition(0, positions[0]);
        _lineRenderer.SetPosition(1, positions[1]);
    }

    private void UpdateLine(Vector2 pos)
    {
        positions.Add(pos);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount -1, pos);
    }
}
