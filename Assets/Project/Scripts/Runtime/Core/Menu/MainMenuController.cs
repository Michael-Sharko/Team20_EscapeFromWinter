using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Winter.Assets.Project.Scripts.Runtime.Services.Audio;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MusicPlayer _musicPlayer;

        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitButton;

        [Header("Autors Panel")]
        [SerializeField] private GameObject _autorsPanel;
        [SerializeField] private Button _openAutorsPanel;
        [SerializeField] private Button _closeAutorsPanel;

        [Header("Settings Panel")]
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Button _openSettingsPanelButton;
        [SerializeField] private Button _closeSettingsPanelButton;

        [Header("Select Level Settings")]
        [SerializeField] private Button _selectLevelOne;
        [SerializeField] private Button _selectLevelTwo;
        [SerializeField] private Button _selectLevelThree;
        [SerializeField] private GameObject _panelWithSelectLevelButtons;

        private void Start()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);

            _openSettingsPanelButton.onClick.AddListener(OnOpenSettingsButtonClicked);
            _closeSettingsPanelButton.onClick.AddListener(OnCloseSettingsPanelButtonClicked);

            _openAutorsPanel.onClick.AddListener(OnOpenAutorsPanelButtonClicked);
            _closeAutorsPanel.onClick.AddListener(OnCloseAutorsPanelButtonClicked);

            _selectLevelOne.onClick.AddListener(OnSelectLevelOneButtonLicked);
            _selectLevelTwo.onClick.AddListener(OnSelectLevelTwoButtonLicked);
            _selectLevelThree.onClick.AddListener(OnSelectLevelThreeButtonLicked);

            _musicPlayer.StartMusic();
        }

        private void OnDestroy()
        {
            UnsubscribeButtons();
        }

        private void UnsubscribeButtons()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _openAutorsPanel.onClick.RemoveAllListeners();
            _closeAutorsPanel.onClick.RemoveAllListeners();
            _openSettingsPanelButton.onClick.RemoveAllListeners();
            _closeSettingsPanelButton.onClick.RemoveAllListeners();
            _selectLevelOne.onClick.RemoveAllListeners();
        }

        private void OnCloseSettingsPanelButtonClicked() => _settingsPanel.SetActive(false);

        private void OnOpenSettingsButtonClicked() => _settingsPanel.SetActive(true);

        private void OnOpenAutorsPanelButtonClicked() => _autorsPanel.SetActive(true);

        private void OnCloseAutorsPanelButtonClicked() => _autorsPanel.SetActive(false);

        private void OnStartGameButtonClicked()
        {
            _panelWithSelectLevelButtons.SetActive(true);
            // try
            // {
            //     SceneManager.LoadSceneAsync(_sceneName);
            // }
            // catch
            // {
            //     SceneManager.LoadSceneAsync("TestVB");
            // }
        
        }

        private void OnSelectLevelOneButtonLicked()
        {
            SceneManager.LoadScene("Level1");
            UnsubscribeButtons();
        }

        private void OnSelectLevelTwoButtonLicked()
        {
            SceneManager.LoadScene("Lvl2");
            UnsubscribeButtons();
        }

        private void OnSelectLevelThreeButtonLicked()
        {
            SceneManager.LoadScene("Lvl3");
            UnsubscribeButtons();
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        public void Link(string LinkID)
        {
            Application.OpenURL(LinkID);
        }
    }
}
