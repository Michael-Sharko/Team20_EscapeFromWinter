using System;
using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player
{
    public class PlayerHeadBobService
    {
        public event Action PlayStepSound;

        private Transform _bobCamera;
        private PlayerData _data;
        private float _bobTimer;
        private Vector3 _desiredBobOffset;
        private Vector3 _currentBobOffset;
        private Vector3 _originalBobPosition;
        private bool _canPlayStepSound;
        private float _releaseStepPoint;
        private float _setStepPoint;

        public PlayerHeadBobService(Transform bobCamera, PlayerData data)
        {
            _bobCamera = bobCamera;
            _data = data;

            _originalBobPosition = Vector3.zero;
            _setStepPoint = _data.BobbingStrength * 0.9f;
            _releaseStepPoint = _data.BobbingStrength * 0.5f;
        }

        public void UpdateHeadBob(Vector2 moveDirection, bool isGrounded, bool isPlayerOnSlipperySurface)
        {
            if (moveDirection.magnitude > 0.1f && isGrounded && !isPlayerOnSlipperySurface)
            {
                _bobTimer += Time.deltaTime * _data.BobFrequency;
                float newY = Mathf.Sin(_bobTimer) * _data.BobbingStrength;

                _desiredBobOffset = new Vector3(Mathf.Cos(_bobTimer) * _data.BobbingStrength, _originalBobPosition.y + Mathf.Abs(newY), _originalBobPosition.z);
            }
            else
            {
                _bobTimer = 0;
                _desiredBobOffset = Vector3.Lerp(_desiredBobOffset, Vector3.zero, Time.deltaTime * _data.BobbingFadeSpeed * 0.5f);
            }

            _currentBobOffset = Vector3.Lerp(_currentBobOffset, _desiredBobOffset, Time.deltaTime * _data.BobbingFadeSpeed);
            _bobCamera.localPosition = _currentBobOffset;

            UpdateStepPoint();
        }

        private void UpdateStepPoint()
        {
            if (_currentBobOffset.y >= _setStepPoint)
            {
                _canPlayStepSound = true;
            }
            if (_currentBobOffset.y <= _releaseStepPoint && _canPlayStepSound)
            {
                PlayStepSound?.Invoke();
                _canPlayStepSound = false;
            }
        }
    }
}
