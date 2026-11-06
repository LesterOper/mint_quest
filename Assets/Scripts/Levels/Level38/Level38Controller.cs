using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level38Controller : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBaloons = null;
    public void RestartLevel()
    {
        Debug.Log("ЗАНОВО!");
        Destroy(GameObject.FindWithTag("BaloonsWithThreads"));
        // Установить положение по дефолту камеры
        GameObject.FindWithTag("MainCamera").transform.localPosition = new Vector3(0f,0f, -10f);
        // Установить положение по дефолту бэкграунда
        GameObject.FindWithTag("Background").transform.localPosition = new Vector3(0f,0f, 0f);
        // Установить положение по дефолту бэкграунда
        GameObject.Find("detector").transform.localPosition = new Vector3(0f,0f, 0f);
        // установить положение по дефолту иголки
        GameObject.Find("end_button").transform.localPosition = new Vector3(-1.663348f,-3.929196f);
        // установить положение по дефолту ножниц
        GameObject.Find("scissors").transform.localPosition = new Vector3(-1.91f,-8.74f);
        // установить положение шариков которые летят снизу и запустить анимацию вверх
        Instantiate(_prefabBaloons, new Vector3(0f, -4.22f, 0f), Quaternion.identity);
    
        // По завершению анимации активировать движение камерой
    }
    
}
