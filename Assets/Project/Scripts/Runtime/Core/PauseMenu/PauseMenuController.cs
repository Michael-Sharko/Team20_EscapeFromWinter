using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Winter.Assets.Project.Scripts.Runtime.Utils;

namespace Winter.Assets.Project.Scripts.Runtime.Core.PauseMenu
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _backToMenuButton;

        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Button _settingsPanelOpenButton;
        [SerializeField] private Button _settingsPanelCloseButton;

        private void Start()
        {
            _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);

            _settingsPanelOpenButton.onClick.AddListener(OnSettingsPanelOpenButtonClicked);
            _settingsPanelCloseButton.onClick.AddListener(OnSettingsPanelCloseButtonClicked);

            _pausePanel.SetActive(false);
        }

        private void OnDestroy()
        {
            _backToMenuButton.onClick.RemoveAllListeners();
        }

        private void OnBackToMenuButtonClicked()
        {
            SceneManager.LoadScene(Scenes.MAIN_MENU);
        }

        private void OnSettingsPanelOpenButtonClicked()
        {
            _pausePanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        private void OnSettingsPanelCloseButtonClicked()
        {
            _pausePanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }

        public void HidePausePanel()
        {
            _pausePanel.SetActive(false);
            _settingsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ShowPausePanel()
        {
            _pausePanel.SetActive(true);
            _settingsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
