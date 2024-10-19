using System;
using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;
using Winter.Assets.Project.Scripts.Runtime.Infrastructure.Game.Data;
using Winter.Assets.Project.Scripts.Runtime.Utils;

namespace Winter.Assets.Project.Scripts.Runtime.Services.Settings
{
    public class DataProvider
    {
        private static DataProvider _instance;

        private PlayerData _playerData;
        private GameConfig _gameConfig;

        public static DataProvider Instance => _instance ??= new DataProvider();

        public PlayerData PlayerData => _playerData ?? GetDefaultPlayerData();
        public GameConfig GameConfig => _gameConfig ?? GetDefaultGameConfig();

        public void Initialize(PlayerData playerData, GameConfig gameConfig)
        {
            _playerData = playerData;
            _gameConfig = gameConfig;
        }

        private GameConfig GetDefaultGameConfig()
        {
            return Resources.Load<GameConfig>(Paths.GAME_CONFIG + "/DefaultGameConfig");
        }

        private PlayerData GetDefaultPlayerData()
        {
            return Resources.Load<PlayerData>(Paths.PLAYER_DATA + "/DefaultPlayerData");
        }
    }
}
