using System;
using UnityEngine;

namespace Swimmer.Library.View
{
    [RequireComponent(typeof(LocalizedText))]
    public class CounterText : MonoBehaviour
    {
        [SerializeField] private double _duration = 1f;
        [SerializeField] private double _minChangeSpeed = 20f;

        private LocalizedText _text;

        private bool _initialized;

        private double _changeSpeed;
        private double _currentValue;

        private long _targetValue;
        private long _displayedValue;
        private long _prevDisplayedValue;

        private CompleteHandler _onComplete;
        private ChangeHandler _onValueChanged;

        private bool _completeHandled = true;

        private Type _type;
        private string _formatString;
        private string _formatStringKey;
        private CustomFormatter _customFormatter;


        private void Awake()
        {
            InitializeIfNeed();
        }

        private void InitializeIfNeed()
        {
            if (!_initialized)
            {
                _text = GetComponent<LocalizedText>();

                _initialized = true;
            }
        }

        private void Update()
        {
            if (_displayedValue != _targetValue)
            {
                _currentValue = _displayedValue < _targetValue ?
                    Math.Min(_currentValue + _changeSpeed * Time.deltaTime, _targetValue) :
                    Math.Max(_currentValue - _changeSpeed * Time.deltaTime, _targetValue);

                _displayedValue = (long) _currentValue;

                UpdateText();
            }
        }

        public void ChangeTargetValue(long targetValueChange, bool animated)
        {
            SetTargetValue(_targetValue + targetValueChange, animated);
        }

        public void SetTargetValue(long targetValue, bool animated)
        {
            _completeHandled = false;
            if (animated)
            {
                if (_displayedValue == targetValue)
                    ApplyTargetValueImmediately(targetValue);
                else
                {
                    _targetValue = targetValue;
                    _changeSpeed = Math.Max(_minChangeSpeed, Math.Abs(targetValue - _displayedValue) / _duration);
                }
            }
            else
            {
                ApplyTargetValueImmediately(targetValue);
            }
        }

        private void ApplyTargetValueImmediately(long targetValue)
        {
            _changeSpeed = 0d;
            _currentValue = targetValue;

            _targetValue = targetValue;
            _displayedValue = targetValue;

            UpdateText();
        }

        private void UpdateText()
        {
            InitializeIfNeed();

            switch (_type)
            {
                case Type.BASE:
                    _text.SetExactText(_displayedValue.ToString());
                    break;
                case Type.FORMATTED_STRING:
                    _text.SetExactText(string.Format(_formatString, _displayedValue));
                    break;
                case Type.CUSTOM_FORMATTER:
                    _text.SetExactText(_customFormatter.Invoke(_displayedValue));
                    break;
                case Type.FORMATTED_LOCALIZED_STRING:
                    _text.SetText(_formatStringKey, _displayedValue);
                    break;
                case Type.LOCALIZED_CUSTOM_FORMATTER:
                    _text.SetText(_formatStringKey, _customFormatter.Invoke(_displayedValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_prevDisplayedValue != _displayedValue)
            {
                _onValueChanged?.Invoke(_displayedValue, _displayedValue - _prevDisplayedValue);
            }

            if (_targetValue == _displayedValue && !_completeHandled)
            {
                _completeHandled = true;
                _onComplete?.Invoke();
            }

            _prevDisplayedValue = _displayedValue;
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }

        public void SetFormatString(string formatString)
        {
            _formatString = formatString;
            _type = Type.FORMATTED_STRING;
        }

        public void SetFormatStringKey(string formatStringKey)
        {
            _formatStringKey = formatStringKey;
            _type = Type.FORMATTED_LOCALIZED_STRING;
        }

        public void SetCustomFormatter(CustomFormatter formatter)
        {
            _customFormatter = formatter;
            _type = Type.CUSTOM_FORMATTER;
        }

        public void SetCustomFormatter(string formatStringKey, CustomFormatter formatter)
        {
            _formatStringKey = formatStringKey;
            _customFormatter = formatter;
            _type = Type.LOCALIZED_CUSTOM_FORMATTER;
        }

        public void SetCompleteHandler(CompleteHandler onComplete)
        {
            _onComplete = onComplete;
        }

        public void SetChangeHandler(ChangeHandler onValueChanged)
        {
            _onValueChanged = onValueChanged;
        }

        public delegate string CustomFormatter(long value);
        public delegate void CompleteHandler();
        public delegate void ChangeHandler(long value, long change);

        private enum Type
        {
            BASE,
            FORMATTED_STRING,
            FORMATTED_LOCALIZED_STRING,
            CUSTOM_FORMATTER,
            LOCALIZED_CUSTOM_FORMATTER,
        }
    }
}