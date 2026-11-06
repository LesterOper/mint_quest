using UnityEngine;
using UnityEngine.EventSystems;

namespace Swimmer.Library.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tint2DButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private Color _normalColor;

        private bool _pressed;
        [SerializeField] private Color _pressedColor;

        private SpriteRenderer _spriteRenderer;

        public event ClickHandler OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            UpdatePressedStateColor();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            UpdatePressedStateColor();
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _normalColor = _spriteRenderer.color;
        }

        private void UpdatePressedStateColor()
        {
            _spriteRenderer.color = _pressed ? _pressedColor : _normalColor;
        }

        public Color PressedColor
        {
            get => _pressedColor;
            set
            {
                _pressedColor = value;
                UpdatePressedStateColor();
            }
        }

        public Color NormalColor
        {
            get => _normalColor;
            set
            {
                _normalColor = value;
                UpdatePressedStateColor();
            }
        }

        public delegate void ClickHandler();
    }
}