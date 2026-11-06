using System;
using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.View
{
    public class ResizeProgressBar : MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Right,
            Top,
            Bot
        }

        [SerializeField] private Image _backImage;
        [SerializeField] private Direction _direction;

        private Vector3 _initPosition;
        private Vector2 _initSize;
        [SerializeField] private Image _progressImage;

        private RectTransform _progressImageTransform;

        public Image BackImage => _backImage;
        public Image ProgressImage => _progressImage;

        private void Awake()
        {
            _progressImageTransform = _progressImage.GetComponent<RectTransform>();

            _initPosition = new Vector3(
                _progressImageTransform.localPosition.x,
                _progressImageTransform.localPosition.y,
                _progressImageTransform.localPosition.z);
            _initSize = new Vector2(
                _progressImageTransform.sizeDelta.x,
                _progressImageTransform.sizeDelta.y);
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);

            switch (_direction)
            {
                case Direction.Left:
                    _progressImageTransform.sizeDelta = new Vector2(_initSize.x * progress, _initSize.y);
                    _progressImageTransform.localPosition = new Vector3(
                        _initPosition.x - _initSize.x * (1 - progress) * 0.5f,
                        _initPosition.y,
                        _initPosition.z);
                    break;

                case Direction.Right:
                    _progressImageTransform.sizeDelta = new Vector2(_initSize.x * progress, _initSize.y);
                    _progressImageTransform.localPosition = new Vector3(
                        _initPosition.x + _initSize.x * (1 - progress) * 0.5f,
                        _initPosition.y,
                        _initPosition.z);
                    break;

                case Direction.Top:
                    _progressImageTransform.sizeDelta = new Vector2(_initSize.x, _initSize.y * progress);
                    _progressImageTransform.localPosition = new Vector3(
                        _initPosition.x,
                        _initPosition.y + _initSize.y * (1 - progress) * 0.5f,
                        _initPosition.z);
                    break;

                case Direction.Bot:
                    _progressImageTransform.sizeDelta = new Vector2(_initSize.x, _initSize.y * progress);
                    _progressImageTransform.localPosition = new Vector3(
                        _initPosition.x,
                        _initPosition.y - _initSize.y * (1 - progress) * 0.5f,
                        _initPosition.z);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}