using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player
{
    public class PlayerMotorService
    {
        public Action<float> ClimbingEnduranceUpdated;

        private CharacterController _controller;
        private PlayerData _data;
        private Transform _motorCamera;
        private Vector3 _currentVelocity;
        private Vector3 _currentMoveDirection;
        private float _jumpForce;

        private bool _isCrouching;
        private bool _isSprinting;
        private bool _isClimbing;
        private float _lastHandSwinging;

        private float _climbingEndurance;

        public PlayerMotorService(CharacterController controller, Transform motorCamera, PlayerData data)
        {
            _controller = controller;
            _data = data;
            _motorCamera = motorCamera;

            ClimbingEnduranceUpdated?.Invoke(_climbingEndurance = _data.ClimbingEnduranceMaximum);
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
            else if (_isCrouching)
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

        // todo: to rework!
        public void Climbing(Vector2 moveDirection, float icePickSwinging, bool isFalling, Vector3 wallNormal)
        {
            if (_controller.isGrounded)
            {
                // Debug.Log("MovingOnGround");
                MovingOnGround(moveDirection, isFalling);
            }

            if (IsClimbing(moveDirection, icePickSwinging) && IsUsingDifferentPickaxe(icePickSwinging))
            {
                // Debug.Log("DoClimbing");
                DoClimbing(moveDirection, icePickSwinging, wallNormal);
            }
            else if (_climbingEndurance <= 0 || isFalling)
            {
                // Debug.Log("Fall");
                Fall();
            }
        }

        private void MovingOnGround(Vector2 moveDirection, bool isFalling)
        {
            ClimbingEnduranceUpdated?.Invoke(_climbingEndurance = _data.ClimbingEnduranceMaximum);            
            Move(moveDirection, isFalling);
        }

        private void Fall()
        {
            if (_currentVelocity.y > 0) _currentVelocity.y = 0;
            _lastHandSwinging = .0f;

            // _currentMoveDirection.y = _jumpForce;
            _currentMoveDirection = Vector3.SmoothDamp(_currentMoveDirection, Vector3.down * GetCurrentMoveSpeed(), ref _currentVelocity, _data.SmoothMoveDeltaTime);

            // ApplyJumpForce();

            // _currentMoveDirection.y = _controller.transform.position.y;
            _jumpForce = 0;
            UpdateGravity();

            _controller.Move(_currentMoveDirection * Time.deltaTime);
            // _controller.SimpleMove(_data.Gravity * Time.deltaTime * -_controller.transform.up); // i want to delete this
        }

        private bool IsNotClimbing(Vector2 moveDirection, float icePickSwinging)
        {
            return (icePickSwinging == .0f || moveDirection == Vector2.zero || _lastHandSwinging != .0f && (_lastHandSwinging == icePickSwinging || (_lastHandSwinging + icePickSwinging != 0)));
        }

        private bool IsClimbing(Vector2 moveDirection, float icePickSwinging)
        {
            return
                icePickSwinging != .0f && // using pickaxe
                moveDirection != Vector2.zero; // movement active
        }

        private bool IsUsingDifferentPickaxe(float icePickSwinging)
        {
            return _lastHandSwinging == .0f || // first strike
                _lastHandSwinging + icePickSwinging == 0; // is different hands
        }

        private void DoClimbing(Vector2 inputDirection, float icePickSwinging, Vector3 wallNormal)
        {
            _lastHandSwinging = icePickSwinging;
            SetCrouch(false);

            if (inputDirection.y < 0) inputDirection.y = 0;

            Vector3 sidewaysDirection = Vector3.Cross(Vector3.up, wallNormal).normalized;
            Vector3 upDirection = Vector3.Cross(wallNormal, sidewaysDirection).normalized;

            Vector3 moveDirection = (sidewaysDirection * inputDirection.x + upDirection * inputDirection.y).normalized;

            _currentVelocity = moveDirection * _data.ClimbingMoveSpeed;

            _controller.transform.position += _currentVelocity * Time.deltaTime;

            ClimbingEnduranceUpdated?.Invoke(_climbingEndurance -= 1);
        }
    }
}