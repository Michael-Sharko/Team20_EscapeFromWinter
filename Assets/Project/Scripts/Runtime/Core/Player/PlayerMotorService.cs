using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player
{
    public class PlayerMotorService
    {
        private CharacterController _controller;
        private PlayerData _data;
        private Transform _motorCamera;
        private Vector3 _currentVelocity;
        private Vector3 _currentMoveDirection;
        private float _jumpForce;
        private bool _isCrouching;
        private bool _isSprinting;

        public PlayerMotorService(CharacterController controller, Transform motorCamera, PlayerData data)
        {
            _controller = controller;
            _data = data;
            _motorCamera = motorCamera;
        }

        public void Move(Vector2 moveDirection, bool isJumping)
        {
            float currentMoveSpeed = GetCurrentMoveSpeed();

            Vector3 moveVector = _controller.transform.TransformDirection(new Vector3(moveDirection.x, 0, moveDirection.y)).normalized;

            _currentMoveDirection.y = _jumpForce;
            _currentMoveDirection = Vector3.SmoothDamp(_currentMoveDirection, moveVector * currentMoveSpeed, ref _currentVelocity, _data.SmoothMoveDeltaTime);

            if (isJumping)
                ApplyJumpForce();

            UpdateGravity();

            _controller.Move(_currentMoveDirection * Time.deltaTime);
        }

        private void ApplyJumpForce()
        {
            if (_controller.isGrounded)
            {
                _jumpForce = _data.JumpHeight;
            }
        }

        private void UpdateGravity()
        {
            if (_jumpForce > _data.Gravity)
            {
                _jumpForce += _data.Gravity * Time.deltaTime;
            }
        }

        private float GetCurrentMoveSpeed()
        {
            if (_isSprinting)
                return _data.SprintMoveSpeed;
            if (_isCrouching)
                return _data.CrouchMoveSpeed;

            return _data.MoveSpeed;
        }

        public void SetCrouch(bool isCrouching)
        {
            _isCrouching = isCrouching;
            _controller.height = isCrouching ? 1 : 2; // если мы в присяде то высота контроллера вдвое меньше
            _motorCamera.localPosition = isCrouching ? new Vector3(0, -_data.CrouchHeightDelta, 0) : Vector3.zero; // если мы в присяде то позиция камеры уменьшается на дельту
        }

        public void SetSprint(bool isSprinting)
        {
            _isSprinting = isSprinting;
        }
    }
}