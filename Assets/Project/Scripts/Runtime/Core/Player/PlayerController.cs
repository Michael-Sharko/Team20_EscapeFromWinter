using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;
using Winter.Assets.Project.Scripts.Runtime.Core.TriggerObservable;
using Winter.Assets.Project.Scripts.Runtime.Services.Audio;
using Winter.Assets.Project.Scripts.Runtime.Services.GamePause;
using Winter.Assets.Project.Scripts.Runtime.Services.Input;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player
{
    public class PlayerController : MonoBehaviour, IPauseGameListener, IResumeGameListener
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private SlipperyTriggerObserver _slipperyTriggerObserver;
        [SerializeField] private Transform _bobCamera;
        [SerializeField] private Transform _motorCamera;
        [SerializeField] private Transform _motorObject;

        private PlayerData _data;
        private PlayerMotorService _motorController;
        private PlayerCameraService _cameraController;
        private PlayerHeadBobService _headBobController;
        private InputHandler _inputHandler;
        private SoundsPlayer _audioService;

        private bool _isPlayerOnSlipperySurface;
        private bool _isControllerActive;

        public void Init(InputHandler inputHandler, PlayerData playerData, SoundsPlayer audioService)
        {
            _data = playerData;
            _inputHandler = inputHandler;
            _audioService = audioService;

            _motorController = new PlayerMotorService(_characterController, _motorCamera, _data);
            _cameraController = new PlayerCameraService(_motorCamera, _motorObject, _data);
            _headBobController = new PlayerHeadBobService(_bobCamera, _data);

            _data.SmoothMoveDeltaTime = _data.DefaultSmoothMoveDeltaTime;
            
            _slipperyTriggerObserver.Enter += OnSlipperyTriggerEnter;
            _slipperyTriggerObserver.Exit += OnSlipperyTriggerExit;

            _isControllerActive = true;
            _headBobController.PlayStepSound += _audioService.PlayStepSound;
        }

        public void OnPauseGame() => _isControllerActive = false;

        public void OnResumeGame() => _isControllerActive = true;

        private void OnDestroy()
        {
            _headBobController.PlayStepSound -= _audioService.PlayStepSound;

            _slipperyTriggerObserver.Enter += OnSlipperyTriggerEnter;
            _slipperyTriggerObserver.Exit += OnSlipperyTriggerExit;
        }

        private void Update()
        {
            if (!_isControllerActive)
                return;

            _motorController.Move(_inputHandler.MovementInput, _inputHandler.JumpState);
            _motorController.SetCrouch(_inputHandler.CrouchState);
            _motorController.SetSprint(_inputHandler.SprintState);

            _cameraController.RotateCamera(_inputHandler.RotationInput);
            _headBobController.UpdateHeadBob(_inputHandler.MovementInput, _characterController.isGrounded, _isPlayerOnSlipperySurface);
        }

        private void OnSlipperyTriggerEnter(Collider collider)
        {
            _isPlayerOnSlipperySurface = true;
            _data.SmoothMoveDeltaTime = _data.SlipperySmoothMoveDeltaTime;
        }

        private void OnSlipperyTriggerExit(Collider collider)
        {
            _isPlayerOnSlipperySurface = false;
            _data.SmoothMoveDeltaTime = _data.DefaultSmoothMoveDeltaTime;
        }
    }
}
