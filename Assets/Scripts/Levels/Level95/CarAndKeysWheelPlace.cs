using System;
using UnityEngine;

public class CarAndKeysWheelPlace : MonoBehaviour
{
    [SerializeField] private CarAndKeysController _scrCarAndKeysController = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "CarAndKeysWheel")
        {
            _scrCarAndKeysController.SetWheelOnPlace();
        }
        
        if (other.name == "WheelDisk")
        {
            _scrCarAndKeysController.SetDiskOnPlace();
        }
    }
}
