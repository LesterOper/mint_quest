using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentLevelTextUpdate : MonoBehaviour
{

    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "" +  LevelUtils.GetLastOpenLevel();
    }
}
