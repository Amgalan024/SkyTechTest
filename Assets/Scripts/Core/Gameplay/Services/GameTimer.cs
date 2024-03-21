using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Gameplay
{
    public class GameTimer
    {
        public event Action<DateTime> OnTick;

        public DateTime CurrentTime { get; private set; }

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
            CurrentTime = new DateTime();

            while (_stopped == false)
            {
                await WaitSecond();
                if (_stopped)
                {
                    break;
                }

                CurrentTime = CurrentTime.AddSeconds(1);
                OnTick?.Invoke(CurrentTime);
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