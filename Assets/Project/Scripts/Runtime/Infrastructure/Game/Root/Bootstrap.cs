using UnityEngine;
using UnityEngine.SceneManagement;
using Winter.Assets.Project.Scripts.Runtime.Core.Player.Data;
using Winter.Assets.Project.Scripts.Runtime.Infrastructure.Game.Data;
using Winter.Assets.Project.Scripts.Runtime.Services.Settings;
using Winter.Assets.Project.Scripts.Runtime.Utils;

namespace Winter.Assets.Project.Scripts.Runtime.Infrastructure.Game.Root
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerData _defaultPlayerData;
        [SerializeField] private GameConfig _defaultGameConfig;

        private void Start()
        {
            DataProvider.Instance.Initialize(_defaultPlayerData, _defaultGameConfig);
            SceneManager.LoadScene(Scenes.MAIN_MENU);
        }
    }
}
