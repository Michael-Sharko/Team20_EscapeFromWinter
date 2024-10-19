using System;
using UnityEngine;

namespace Winter.Assets.Project.Scripts.Runtime.Infrastructure.Game.Data
{
    [CreateAssetMenu(fileName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        public Action SoundsVolumeChanged;
        public Action MusicVolumeChanged;

        [field: SerializeField, Min(0.2f)] public float MaxSensitvity { get; private set; } = 20;
        [field: SerializeField, Min(0.2f)] public float MaxSoundsVolume { get; private set; } = 20;
        [field: SerializeField, Min(0.2f)] public float MinSoundsVolume { get; private set; } = -20;
        [field: SerializeField, Min(0.2f)] public float MaxMusicVolume { get; private set; } = 20;
        [field: SerializeField, Min(0.2f)] public float MinMusicVolume { get; private set; } = -20;

        [field: SerializeField, Min(0.2f)] public float MusicVolume { get; set; } = 0;
        [field: SerializeField, Min(0.2f)] public float SoundsVolume { get; set; } = 0;
        
    }
}
