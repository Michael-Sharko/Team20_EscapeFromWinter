using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player
{
    public class PlayerCameraService
    {
        private Transform _motorCamera;
        private float _yRotation;
        private Transform _motorObject;
        private PlayerData _data;

        public PlayerCameraService(Transform motorCamera, Transform motorObject, PlayerData playerData)
        {
            _motorCamera = motorCamera;
            _motorObject = motorObject;
            _data = playerData;
        }

        public void RotateCamera(Vector2 rotation)
        {
            rotation = _data.RotateSpeed * Time.deltaTime * rotation;
            _yRotation = Mathf.Clamp(_yRotation - rotation.y, -70f, 90f);

            _motorCamera.localRotation = Quaternion.Euler(_yRotation, _motorCamera.localRotation.y, _motorCamera.localRotation.z);
            _motorObject.Rotate(Vector3.up * rotation.x);
        }
    }
}
