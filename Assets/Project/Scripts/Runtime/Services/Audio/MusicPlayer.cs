using UnityEngine;
using UnityEngine.Audio;
using Winter.Assets.Project.Scripts.Runtime.Services.Settings;

namespace Winter.Assets.Project.Scripts.Runtime.Services.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioClip _backgroundMusicClip;

        private void Start()
        {
            DataProvider.Instance.GameConfig.MusicVolumeChanged += SetMusicVolume;

            _mixer.SetFloat("MusicVolume", DataProvider.Instance.GameConfig.MusicVolume);
        }

        private void OnDestroy()
        {
            DataProvider.Instance.GameConfig.MusicVolumeChanged -= SetMusicVolume;
        }

        public void StartMusic()
        {
            _musicSource.clip = _backgroundMusicClip;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private void SetMusicVolume()
        {
            _mixer.SetFloat("MusicVolume", DataProvider.Instance.GameConfig.MusicVolume);
        }
    }
}
