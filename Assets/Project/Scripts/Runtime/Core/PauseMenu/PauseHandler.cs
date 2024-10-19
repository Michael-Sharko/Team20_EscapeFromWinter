using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Services.GamePause;
using Winter.Assets.Project.Scripts.Runtime.Services.Input;

namespace Winter.Assets.Project.Scripts.Runtime.Core.PauseMenu
{
    public class PauseHandler : MonoBehaviour, IEndGameListener
    {
        [SerializeField] private PauseMenuController _pauseMenuController;

        private InputHandler _inputHandler;
        private GamePauseService _gameObserver;
        private bool _isMenuOpened;
        private bool _isPauseCapableToEnable;

        public void Init(InputHandler inputHandler, GamePauseService observer)
        {
            _inputHandler = inputHandler;
            _gameObserver = observer;

            _isMenuOpened = false;
            _isPauseCapableToEnable = true;

            _inputHandler.UpdatePauseState += OnUpdatePauseState;
        }

        public void OnEndGame()
        {
            _isPauseCapableToEnable = false;
        }

        private void OnDestroy()
        {
            if (_inputHandler == null)
                return;

            _inputHandler.UpdatePauseState -= OnUpdatePauseState;
        }

        public void OnUpdatePauseState()
        {
            if (!_isPauseCapableToEnable)
                return;

            if (_isMenuOpened)
            {
                _pauseMenuController.HidePausePanel();
                _gameObserver.ResumeGame();
            }
            else
            {
                _pauseMenuController.ShowPausePanel();
                _gameObserver.PauseGame();
            }

            _isMenuOpened = !_isMenuOpened;
        }
    }
}
