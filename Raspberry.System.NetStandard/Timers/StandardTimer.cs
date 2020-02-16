using System;
using SystemTimer = System.Threading.Timer;

namespace Raspberry.System.NetStandard.Timers
{
    /// <summary>
    /// Represents a timer.
    /// </summary>
    public class StandardTimer : ITimer
    {
        #region Fields

        private TimeSpan _interval;
        private Action _action;

        private bool _isStarted;
        private SystemTimer _timer;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the interval, in milliseconds.
        /// </summary>
        /// <value>
        /// The interval, in milliseconds.
        /// </value>
        public TimeSpan Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                if (_isStarted)
                    Start(TimeSpan.Zero);
            }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public Action Action
        {
            get => _action;
            set
            {
                if (value == null)
                    Stop();

                _action = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="startDelay">The delay before the first occurence, in milliseconds.</param>
        public void Start(TimeSpan startDelay)
        {
            lock (this)
            {
                if (!_isStarted && _interval.TotalMilliseconds >= 1)
                {
                    _isStarted = true;
                    _timer = new SystemTimer(OnElapsed, null, startDelay, _interval);
                }
                else
                    Stop();
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            lock (this)
            {
                if (_isStarted)
                {
                    _isStarted = false;
                    _timer.Dispose();
                    _timer = null;
                }
            }
        }

        #endregion

        #region Private Helpers

        private void NoOp(){}

        private void OnElapsed(object state)
        {
            (Action ?? NoOp)();
        }

        #endregion
    }
}