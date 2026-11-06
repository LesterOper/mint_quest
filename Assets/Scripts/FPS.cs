using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
 
public class FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fps;
    private static float fps;
 
    void Update()
    {
        fps = 1.0f / Time.deltaTime;
        _fps.text = "FPS: " + (int)fps;
    }
}