using System;
using System.Collections.Generic;

namespace Winter.Assets.Project.Scripts.Runtime.Services.GamePause
{
    public sealed class GamePauseService : IDisposable
    {
        private readonly List<object> _listeners = new();

        public void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IPauseGameListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IResumeGameListener pauseListener)
                {
                    pauseListener.OnResumeGame();
                }
            }
        }

        public void EndGame()
        {
            PauseGame();

            foreach (var listener in _listeners)
            {
                if (listener is IEndGameListener pauseListener)
                {
                    pauseListener.OnEndGame();
                }
            }

            _listeners.Clear();
        }

        public void AddListener(object listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(object listener)
        {
            _listeners.Remove(listener);
        }

        public void Dispose()
        {
            _listeners.Clear();
        }
    }
}