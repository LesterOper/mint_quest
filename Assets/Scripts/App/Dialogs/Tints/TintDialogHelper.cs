using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TintDialogHelper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject dialogParent;
    [SerializeField] private GameObject dialogPrefab;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        var dialog = Instantiate(dialogPrefab, dialogParent.transform);
    }
}
