using System;
using UnityEngine;

namespace Ui
{
    internal class Pause : IDisposable
    {
        private const float PausedTimeScale = 0f;

        private readonly float _initialTimeScale;

        private bool _isDisposed;
        private bool _isEnabled;


        public Pause() =>
            _initialTimeScale = Time.timeScale;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            if (_isEnabled)
                Disable();

            _isDisposed = true;
        }

        public void Enable()
        {
            _isEnabled = true;
            Time.timeScale = PausedTimeScale;
        }

        public void Disable()
        {
            _isEnabled = false;
            Time.timeScale = _initialTimeScale;
        }
    }
}
