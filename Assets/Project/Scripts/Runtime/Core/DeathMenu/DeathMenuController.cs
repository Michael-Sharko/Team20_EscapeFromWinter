using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Winter.Assets.Project.Scripts.Runtime.Utils;

namespace Winter.Assets.Project.Scripts.Runtime.Core.DeathMenu
{
    public class DeathMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _reloadLevelButton;

        private void Start()
        {
            _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
            _reloadLevelButton.onClick.AddListener(OnReloadLevelButtonCLicked);

            _pausePanel.SetActive(false);
        }

        public void ShowPanel()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            
            _pausePanel.SetActive(true);
        }

        private void OnDestroy()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
            _reloadLevelButton.onClick.RemoveAllListeners();
        }

        private void OnBackToMenuButtonClicked()
        {
            SceneManager.LoadScene(Scenes.MAIN_MENU);
        }

        private void OnReloadLevelButtonCLicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
