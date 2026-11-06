using System;
using UnityEngine;

namespace Swimmer.Library.View
{
    [RequireComponent(typeof(LocalizedText))]
    public class CountdownTimer : MonoBehaviour
    {
        private const long UPDATE_INTERVAL = Constants.TicksPerMillisecond * 100;
        private const long MIN_REWIND_SPEED = 10 * TimeSpan.TicksPerMinute;
        private const long MAX_REWIND_DURATION = 5L * TimeSpan.TicksPerSecond;

        private long _durationTicks;

        private bool _inProgress;

        private long _startTimeTicks;
        private bool _stopped;

        private LocalizedText _text;

        [Tooltip("d:h:m:s")] [SerializeField] private string _timeFormatDays;
        [Tooltip("h:m:s")] [SerializeField] private string _timeFormatHours;
        [Tooltip("m:s")] [SerializeField] private string _timeFormatMinutes;

        private long _timeLeft;

        private long _updatedTimeLeft = long.MinValue;

        public long DisplayTimeScale { get; set; } = 1;

        private long _rewindSpeed;

        public event CountdownEndHandler OnCountdownEnd;

        private void Awake()
        {
            if (string.IsNullOrEmpty(_timeFormatDays))
                _timeFormatDays = "{0:00}:{1:00}:{2:00}:{3:00}";
            if (string.IsNullOrEmpty(_timeFormatHours))
                _timeFormatHours = "{0:00}:{1:00}:{2:00}";
            if (string.IsNullOrEmpty(_timeFormatMinutes))
                _timeFormatMinutes = "{0:00}:{1:00}";
        }

        public void StartCountdown(long startTimeTicks, long durationTicks)
        {
            _startTimeTicks = startTimeTicks;
            _durationTicks = durationTicks;

            _inProgress = true;
            _stopped = false;

            _rewindSpeed = 0;

            Update();
        }

        public void ChangeStartTime(long startTime)
        {
            var change = startTime - _startTimeTicks;
            _rewindSpeed = change / Math.Min(MAX_REWIND_DURATION, Math.Abs(change) / MIN_REWIND_SPEED);

            _startTimeTicks = startTime;
        }

        public void StopCountdown()
        {
            _stopped = true;
        }

        public bool IsActive()
        {
            return _inProgress && !_stopped;
        }

        private void Update()
        {
            if (_inProgress && !_stopped)
            {
                if (_text == null)
                    _text = GetComponent<LocalizedText>();

                if (_rewindSpeed != 0)
                {
                    if (_updatedTimeLeft < 0)
                    {
                        _rewindSpeed = 0;
                        return;
                    }

                    var realTimeLeft = _durationTicks - (DateTime.UtcNow.Ticks - _startTimeTicks);
                    _timeLeft = (long) (_updatedTimeLeft + (double) Time.deltaTime * _rewindSpeed);

                    if (_rewindSpeed < 0 && _timeLeft <= realTimeLeft)
                    {
                        _timeLeft = realTimeLeft;
                        _rewindSpeed = 0;
                    }
                    else if (_rewindSpeed > 0 && _timeLeft >= realTimeLeft)
                    {
                        _timeLeft = realTimeLeft;
                        _rewindSpeed = 0;
                    }
                }
                else
                {
                    _timeLeft = _durationTicks - (DateTime.UtcNow.Ticks - _startTimeTicks);
                }

                if (_timeLeft > 0)
                {
                    if (Math.Abs(_timeLeft - _updatedTimeLeft) >= UPDATE_INTERVAL)
                    {
                        var displayTimeLeft = _timeLeft * DisplayTimeScale;

                        if (displayTimeLeft > Constants.TicksPerDay)
                            _text.SetText(_timeFormatDays,
                                displayTimeLeft / Constants.TicksPerDay,
                                displayTimeLeft % Constants.TicksPerDay / Constants.TicksPerHour,
                                displayTimeLeft % Constants.TicksPerHour / Constants.TicksPerMinute,
                                displayTimeLeft % Constants.TicksPerMinute / Constants.TicksPerSecond);
                        else if (displayTimeLeft > Constants.TicksPerHour)
                            _text.SetText(_timeFormatHours,
                                displayTimeLeft / Constants.TicksPerHour,
                                displayTimeLeft % Constants.TicksPerHour / Constants.TicksPerMinute,
                                displayTimeLeft % Constants.TicksPerMinute / Constants.TicksPerSecond);
                        else
                            _text.SetText(_timeFormatMinutes,
                                displayTimeLeft / Constants.TicksPerMinute,
                                displayTimeLeft % Constants.TicksPerMinute / Constants.TicksPerSecond);

                        _updatedTimeLeft = _timeLeft;
                    }
                }
                else
                {
                    _text.SetText(_timeFormatMinutes, 0, 0);
                    _inProgress = false;

                    if (OnCountdownEnd != null)
                        OnCountdownEnd();
                }
            }
        }

        public delegate void CountdownEndHandler();
    }
}