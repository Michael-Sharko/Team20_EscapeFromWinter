using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;
using Winter.Assets.Project.Scripts.Runtime.Services.Audio;
using Winter.Assets.Project.Scripts.Runtime.Services.Input;
using Winter.Assets.Project.Scripts.Runtime.Services.Settings;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player
{
    public class PlayerBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerController _controller;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private SoundsPlayer _soundsPlayer;

        [Header("Self Activate")]
        [SerializeField] private bool _isSelfActivate;
        [SerializeField] private PlayerData _customPlayerData;

        private void Start()
        {
            if(_isSelfActivate)
            {
                _inputHandler.Enable();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Init();
            }
        }

        public void Init()
        {
            PlayerData playerData = _customPlayerData;

            if(DataProvider.Instance.PlayerData != null)
                playerData = DataProvider.Instance.PlayerData;

            _controller.Init(_inputHandler, playerData, _soundsPlayer);
        }
    }
}
