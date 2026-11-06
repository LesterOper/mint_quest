using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElectricityHelper : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private DragController _controller;
    [SerializeField] private List<TriggersDetector> _detectors;
    
    public void CheckTriggers()
    {
        int count = _detectors.Count(obj => obj.isStay);
        if (count == _detectors.Count)
        {
            _controller.SetTarget(target);
        }
        else
        {
            _controller.SetTarget(null);
        }
    }
}
