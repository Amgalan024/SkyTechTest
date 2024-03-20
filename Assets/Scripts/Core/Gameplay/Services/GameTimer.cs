using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Gameplay
{
    public class GameTimer
    {
        public event Action<int> OnTick;

        private int _currentTime;

        private bool _paused;
        private bool _stopped;

        public void Start()
        {
            _stopped = false;
            _paused = false;
            StartTimerAsync().Forget();
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void Stop()
        {
            _stopped = true;
        }

        private async UniTask StartTimerAsync()
        {
            _currentTime = 0;

            while (_stopped == false)
            {
                await WaitSecond();
                if (_stopped)
                {
                    break;
                }

                _currentTime++;
                OnTick?.Invoke(_currentTime);
            }
        }

        private async UniTask WaitSecond()
        {
            var fixedFrames = 0f;

            while (fixedFrames <= 1)
            {
                if (_paused == false)
                {
                    await UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
                    fixedFrames += Time.fixedDeltaTime;
                }
                else
                {
                    await UniTask.WaitWhile(() => _paused);
                }
            }
        }
    }
}