using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawOnScreen : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject parent;
    public GameObject linePref;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    public List<Vector2> fingerPositions;
    public List<LineRenderer> lines;
    public List<BoxCollider2D> triggers;


    private bool isEnable = true;

    private void Update()
    {
        if(!isEnable) return;
        int count = 0;
        lines.ForEach(obj => { count += obj.positionCount; });
        if (count >= 1000)
        {
            int countDesableColliders = triggers.Count(obj => !obj.enabled);
            if (countDesableColliders == triggers.Count)
            {
                isEnable = false;
                Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
            }
        }
    }
    

    private void CreateLine()
    {
        currentLine = Instantiate(linePref, Vector3.zero, Quaternion.identity);
        currentLine.transform.SetParent(parent.transform);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider2D = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        lines.Add(lineRenderer);
        edgeCollider2D.points = fingerPositions.ToArray();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(mousePos, fingerPositions[fingerPositions.Count - 1]) > .1f)
        {
            UpdateLine(mousePos);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
        CreateLine();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    private void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount -1, newFingerPos);
        edgeCollider2D.points = fingerPositions.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggers.ForEach(obj =>
        {
            if (obj.gameObject.name == other.name)
            {
                obj.enabled = false;
            }
        });
    }
}
