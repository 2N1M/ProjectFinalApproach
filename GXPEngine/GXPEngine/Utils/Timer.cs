using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    /// <summary>
    /// Set a timer in seconds
    /// </summary>
    public class Timer : GameObject
    {
        private readonly Action<object> callback;
        private readonly object state;

        private bool start = false;

        private float _elapsedTime = 0.0f;
        private float _timeInSeconds = 1.0f;

        private bool _timeDirection = true;

        public Timer(Action<object> callback, object state, float setTimeInSeconds)
        {
            this.callback = callback;
            this.state = state;

            _timeInSeconds = setTimeInSeconds;
            game.AddChild(this);
        }

        public float Duration { get { return _timeInSeconds; } }

        public bool TimeDirection
        {
            get
            {
                return _timeDirection;
            }
            set
            {
                _timeDirection = value;
            }
        }

        public bool Over
        {
            get
            {
                if (_timeDirection)
                {
                    return (ElapsedTime >= _timeInSeconds);
                }
                else
                {
                    return (ElapsedTime <= 0);
                }
            }
        }

        public float ElapsedTime
        {
            get { return _elapsedTime; }
        }

        public float AmountCompleted
        {
            get { return ElapsedTime / _timeInSeconds; }
        }
        public void Start()
        {
            this.start = true;
        }

        public void Reset()
        {
            _elapsedTime = 0.0f;
        }

        void Update()
        {
            if (start)
            {
                if (_timeDirection)
                {
                    if (_elapsedTime < _timeInSeconds)
                    {
                        _elapsedTime += (Time.DeltaTime / 1000f);
                    }
                    else
                    {
                        callback(state);
                        start = false;
                        Reset();
                    }
                }
                else
                {
                    if (_elapsedTime > 0)
                    {
                        _elapsedTime -= (Time.DeltaTime / 1000f);
                    }
                    else
                    {
                        callback(state);
                        start = false;
                        Reset();
                    }
                }
            }
        }
    }
}
