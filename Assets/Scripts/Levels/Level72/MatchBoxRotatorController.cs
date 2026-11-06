using UnityEngine;
using UnityEngine.EventSystems;

public class MatchBoxRotatorController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MatchAndFireController _matchboxController = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        _matchboxController.RotateMatchbox();
    }
}
