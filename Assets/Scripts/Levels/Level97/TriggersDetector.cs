using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersDetector : MonoBehaviour
{
    [SerializeField] private BoxCollider2D trigger;
    public bool isStay = false;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (trigger == other)
        {
            isStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (trigger == other)
        {
            isStay = false;
        }
    }
}
