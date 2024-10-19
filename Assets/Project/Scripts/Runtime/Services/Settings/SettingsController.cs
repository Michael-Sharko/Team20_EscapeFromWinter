using UnityEngine;
using UnityEngine.UI;

namespace Winter.Assets.Project.Scripts.Runtime.Services.Settings
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private Slider _sensitivitySlider;
        [SerializeField] private Slider _soundsVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;

        private void Start()
        {
            _sensitivitySlider.minValue = 0.1f;
            _soundsVolumeSlider.minValue = DataProvider.Instance.GameConfig.MinSoundsVolume;
            _musicVolumeSlider.minValue = DataProvider.Instance.GameConfig.MinMusicVolume;
            
            _soundsVolumeSlider.maxValue = DataProvider.Instance.GameConfig.MaxSoundsVolume;
            _musicVolumeSlider.maxValue = DataProvider.Instance.GameConfig.MaxMusicVolume;
            _sensitivitySlider.maxValue = DataProvider.Instance.GameConfig.MaxSensitvity;
            
            _sensitivitySlider.value = DataProvider.Instance.PlayerData.RotateSpeed;
            _soundsVolumeSlider.value = DataProvider.Instance.GameConfig.SoundsVolume;
            _musicVolumeSlider.value = DataProvider.Instance.GameConfig.MusicVolume;

            _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
            _soundsVolumeSlider.onValueChanged.AddListener(OnSoundsVolumeChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        private void OnDestroy()
        {
            _sensitivitySlider.onValueChanged.RemoveAllListeners();
            _soundsVolumeSlider.onValueChanged.RemoveAllListeners();
            _musicVolumeSlider.onValueChanged.RemoveAllListeners();
        }

        private void OnMusicVolumeChanged(float volume)
        {
            DataProvider.Instance.GameConfig.MusicVolume = volume;
            DataProvider.Instance.GameConfig.MusicVolumeChanged?.Invoke();
        }

        private void OnSoundsVolumeChanged(float volume)
        {
            DataProvider.Instance.GameConfig.SoundsVolume = volume;
            DataProvider.Instance.GameConfig.SoundsVolumeChanged?.Invoke();
        }

        private void OnSensitivityChanged(float value)
        {
            DataProvider.Instance.PlayerData.RotateSpeed = value;
        }
    }
}
