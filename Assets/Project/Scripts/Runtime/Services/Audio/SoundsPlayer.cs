using UnityEngine;
using UnityEngine.Audio;
using Winter.Assets.Project.Scripts.Runtime.Services.Settings;

namespace Winter.Assets.Project.Scripts.Runtime.Services.Audio
{
    public class SoundsPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioClip _stepClip;

        private void Start()
        {
            DataProvider.Instance.GameConfig.SoundsVolumeChanged += SetSoundsVolume;

            _mixer.SetFloat("SoundsVolume", DataProvider.Instance.GameConfig.SoundsVolume);
        }

        private void OnDestroy()
        {
            DataProvider.Instance.GameConfig.SoundsVolumeChanged -= SetSoundsVolume;
        }

        public void PlayStepSound()
        {
            _soundSource.PlayOneShot(_stepClip);
        }

        private void SetSoundsVolume()
        {
            _mixer.SetFloat("SoundsVolume", DataProvider.Instance.GameConfig.SoundsVolume);
        }
    }
}
