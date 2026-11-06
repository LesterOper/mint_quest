using Swimmer.Library.Utils;
using Swimmer.Library.View;
using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.Dialogs
{
    public class ChooseCountView : MonoBehaviour
    {
        public delegate void CountChangeHandler(long count, float rate);

        public delegate long ResultValueCalculator(long count, float rate);

        private ResultValueCalculator _calcResultValue;

        [SerializeField] private LocalizedText _countText;
        [SerializeField] private string _countTextKey;

        [SerializeField] private Button _lessButton;
        private long _maxCount;
        private long _minCount;
        [SerializeField] private Button _moreButton;

        [Tooltip("Can be null")] [SerializeField]
        private LocalizedText _rateText;

        [SerializeField] private string _rateTextKey;

        [SerializeField] private string _resultTextKey;

        [Tooltip("Can be null")] [SerializeField]
        private LocalizedText _resultValueText;

        public long Count { get; private set; }

        public float Rate { get; private set; }

        public string RateTextKey
        {
            get => _rateTextKey;
            set
            {
                _rateTextKey = value;
                UpdateView();
            }
        }

        public string CountTextKey
        {
            get => _countTextKey;
            set
            {
                _countTextKey = value;
                UpdateView();
            }
        }

        public string ResultTextKey
        {
            get => _resultTextKey;
            set
            {
                _resultTextKey = value;
                UpdateView();
            }
        }

        public LocalizedText CountText => _countText;

        public long ChangeStep { get; set; } = 0;

        public event CountChangeHandler OnCountChanged;

        protected virtual void Start()
        {
            _lessButton.onClick.AddListener(Less);
            _moreButton.onClick.AddListener(More);
        }

        public void Setup(float rate, long minCount, long maxCount, ResultValueCalculator calcResultValue = null)
        {
            Rate = rate;

            if (_rateText != null)
                _rateText.SetText(_rateTextKey, rate);

            _minCount = minCount;
            _maxCount = maxCount;

            _calcResultValue = calcResultValue;

            Count = _maxCount;

            UpdateView();

            OnCountChanged?.Invoke(Count, Rate);
        }

        protected virtual void UpdateView()
        {
            if (!string.IsNullOrEmpty(_countTextKey))
                _countText.SetText(_countTextKey, Count);
            else
                _countText.SetText("{0:0}", Count.ToString());

            Debug.Log("[CCV] UpdateView : " + Count + "; " + (_resultValueText != null) + "; " +
                      "" + (_calcResultValue == null) + "; " + MathA.Ceil(Count * Rate) + "; ");

            if (_resultValueText != null)
            {
                var resultValue = _calcResultValue == null
                    ? MathA.Ceil(Count * Rate)
                    : _calcResultValue(Count, Rate);
                _resultValueText.SetText(_resultTextKey, resultValue);
            }
        }

        private void More()
        {
            if (ChangeStep != 0)
                SetCount(Count + ChangeStep);
            else if (Count < 10)
                SetCount(Count + 1);
            else if (Count < 100)
                SetCount(Count + 10);
            else if (Count < 1000)
                SetCount(Count + 100);
            else if (Count < 10000)
                SetCount(Count + 1000);
            else if (Count < 100000)
                SetCount(Count + 10000);
            else if (Count < 1000000)
                SetCount(Count + 100000);
            else if (Count < 10000000)
                SetCount(Count + 1000000);
            else if (Count < 100000000)
                SetCount(Count + 10000000);
            else
                SetCount(Count + 100000000);
        }

        private void Less()
        {
            if (ChangeStep != 0)
                SetCount(Count - ChangeStep);
            else if (Count <= 10)
                SetCount(Count - 1);
            else if (Count <= 100)
                SetCount(Count - 10);
            else if (Count <= 1000)
                SetCount(Count - 100);
            else if (Count <= 10000)
                SetCount(Count - 1000);
            else if (Count <= 100000)
                SetCount(Count - 10000);
            else if (Count <= 1000000)
                SetCount(Count - 100000);
            else if (Count <= 10000000)
                SetCount(Count - 1000000);
            else if (Count <= 100000000)
                SetCount(Count - 10000000);
            else
                SetCount(Count - 100000000);
        }

        public void SetCount(long count)
        {
            Count = MathA.Clamp(count, _minCount, _maxCount);

            UpdateView();
            OnCountChanged?.Invoke(Count, Rate);
        }
    }
}