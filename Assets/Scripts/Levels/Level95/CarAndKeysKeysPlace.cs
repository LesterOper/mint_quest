using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAndKeysKeysPlace : MonoBehaviour
{
    [SerializeField] private CarAndKeysController _scrCarAndKeysController = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.name == "CarAndKeysKey1")
        {
            if (_scrCarAndKeysController.GetRideStatus() == CarAndKeysController.CarStatus.WALL_WAS_BROCKEN)
            {
                _scrCarAndKeysController.OpenDoor();
            }
        }
    }
}
