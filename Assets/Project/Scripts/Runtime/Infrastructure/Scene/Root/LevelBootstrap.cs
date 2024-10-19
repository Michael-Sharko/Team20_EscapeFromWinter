using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Death;
using Winter.Assets.Project.Scripts.Runtime.Core.FreezeSystem;
using Winter.Assets.Project.Scripts.Runtime.Core.Intro;
using Winter.Assets.Project.Scripts.Runtime.Core.PauseMenu;
using Winter.Assets.Project.Scripts.Runtime.Core.Player;
using Winter.Assets.Project.Scripts.Runtime.Services.Audio;
using Winter.Assets.Project.Scripts.Runtime.Services.GamePause;
using Winter.Assets.Project.Scripts.Runtime.Services.Input;

namespace Winter.Assets.Project.Scripts.Runtime.Infrastructure.Scene.Root
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerBootstrap _playerBootstrap;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private PauseHandler _pauseHandler;
        [SerializeField] private FreezeController _freezeController;
        [SerializeField] private StrelkaRotationUpdater _strelkaController;
        [SerializeField] private MusicPlayer _musicPlayer;

        [SerializeField] private IntroController _introController;
        [SerializeField] private bool _isIntroNeeded;

        [Header("Death References")]
        [SerializeField] private DeathHandler _deathHandler;
        [SerializeField] private DeathTrigger[] _deathTriggers;

        private FreezeModel _freezeModel;
        private GamePauseService _gamePauseService;

        void Start()
        {
            SetCursorInvisible();
            InitGamePauseService();
            InitFreezeModel();
            InitDeathConditions();
            InitIntro();

            _gamePauseService.PauseGame();

            _playerBootstrap.Init();
            _freezeController.Init(_freezeModel);
            _deathHandler.Init(_gamePauseService);

            _musicPlayer.StartMusic();
            _inputHandler.Enable();
        }

        private void OnDestroy()
        {
            _gamePauseService?.Dispose();

            if(_freezeModel != null)
            {
                _freezeModel.FreezeMax -= _deathHandler.SetDeath;
                _freezeModel.FreezeValueChanged -= _strelkaController.UpdateStrelkaRotation;
            }

            foreach (var trigger in _deathTriggers)
            {
                trigger.Enter -= _deathHandler.SetDeath;
            }
        }

        private void InitGamePauseService()
        {
            _gamePauseService = new GamePauseService();
            _gamePauseService.AddListener(_playerController);
            _gamePauseService.AddListener(_freezeController);
            _gamePauseService.AddListener(_pauseHandler);
        }

        private void SetCursorInvisible()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void InitFreezeModel()
        {
            _freezeModel = new FreezeModel();
            _freezeModel.FreezeValueChanged += _strelkaController.UpdateStrelkaRotation;
        }

        private void InitDeathConditions()
        {
            _freezeModel.FreezeMax += _deathHandler.SetDeath;

            foreach (var trigger in _deathTriggers)
            {
                trigger.Enter += _deathHandler.SetDeath;
            }
        }

        private void InitIntro()
        {
            if(_isIntroNeeded)
            {
                _inputHandler.SkipIntroPerformed += SkipIntro;
                _introController.Show();
            }
            else
            {
                ResumeGame();
            }
                
        }

        private void SkipIntro()
        {
            _inputHandler.SkipIntroPerformed -= SkipIntro;

            _introController.Hide();
            ResumeGame();
        }

        private void ResumeGame()
        {
            _pauseHandler.Init(_inputHandler, _gamePauseService);

            _gamePauseService.ResumeGame();
        }
    }
}
