using UnityEngine;
using UnityEngine.EventSystems;

namespace Swimmer.Library.View
{
    public class Physics2DClickDetector : MonoBehaviour, IPointerClickHandler
    {
        private bool _pressed;

        public event ClickHandler OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }

        public delegate void ClickHandler();
    }
}