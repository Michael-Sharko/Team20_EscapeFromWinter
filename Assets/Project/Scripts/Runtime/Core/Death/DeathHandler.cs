using UnityEngine;
using Winter.Assets.Project.Scripts.Runtime.Core.DeathMenu;
using Winter.Assets.Project.Scripts.Runtime.Services.GamePause;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Death
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private DeathMenuController _deathMenuController;
        private GamePauseService _gamePauseService;

        public void Init(GamePauseService gamePauseService)
        {
            _gamePauseService = gamePauseService;
        }

        public void SetDeath()
        {
            _gamePauseService.EndGame();

            _deathMenuController.ShowPanel();
        }
    }
}
