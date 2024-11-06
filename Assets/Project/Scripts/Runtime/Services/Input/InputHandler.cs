using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControls;

namespace Winter.Assets.Project.Scripts.Runtime.Services.Input
{
    [CreateAssetMenu(fileName = "Input Service")]
    public class InputHandler : ScriptableObject, IGameplayActions
    {
        public event Action UpdatePauseState;
        public event Action SkipIntroPerformed;
        public Action InstrumentSwitched;

        private PlayerControls _inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 RotationInput { get; private set; }
        public float IcePickSwingingInput { get; private set; }
        public bool JumpState { get; private set; }
        public bool CrouchState { get; private set; }
        public bool SprintState { get; private set; }

        public void Enable()
        {
            _inputActions ??= new PlayerControls();
            _inputActions.Enable();
            _inputActions.Gameplay.SetCallbacks(this);
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            CrouchState = context.performed;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpState = context.performed;
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            SprintState = context.performed;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            RotationInput = context.ReadValue<Vector2>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            UpdatePauseState?.Invoke();
        }

        public void OnSkipIntro(InputAction.CallbackContext context)
        {
            SkipIntroPerformed?.Invoke();
        }

        public void OnActivateIcePicks(InputAction.CallbackContext context)
        {
            InstrumentSwitched?.Invoke();
        }

        public void OnIcePickSwinging(InputAction.CallbackContext context)
        {
            IcePickSwingingInput = context.ReadValue<float>();
        }
    }
}
