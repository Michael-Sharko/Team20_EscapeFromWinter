using UnityEngine;

namespace Winter.Assets.Project.Scripts.Runtime.Core.Player.Data
{
    [CreateAssetMenu(fileName = "Player Data")]
    public class PlayerData : ScriptableObject
    {
        [field: Header(" Motor Settings")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 7f;
        [field: SerializeField] public float Gravity { get; private set; } = -19.6f;
        [field: SerializeField] public float JumpHeight { get; private set; } = 8f;
        [field: SerializeField, Min(0.1f)] public float RotateSpeed { get; set; } = 5f;
        [field: SerializeField] public float SmoothMoveDeltaTime { get; set; } = 0.1f;
        [field: SerializeField] public float DefaultSmoothMoveDeltaTime { get; private set; } = 0.1f;
        [field: SerializeField] public float SlipperySmoothMoveDeltaTime { get; private set; } = 0.8f;

        [field: Header("Head Bob Settings")]
        [field: SerializeField] public float BobbingFadeSpeed { get; private set; } = 10f;
        [field: SerializeField] public float BobbingStrength { get; private set; } = 0.3f; // Высота подпрыгивания
        [field: SerializeField] public float BobFrequency { get; private set; } = 5f;

        [field: Header("Crouch Settings")]
        [field: SerializeField, Range(0, 1.5f)] public float CrouchHeightDelta { get; private set; } = 1f;
        [field: SerializeField] public float CrouchMoveSpeed { get; private set; } = 4f;

        [field: Header("Sprint Settings")]
        [field: SerializeField] public float SprintMoveSpeed { get; private set; } = 9f;

        [field: Header("Climbing Settings")]
        [field:SerializeField] public float ClimbingMoveSpeed { get; private set; } = 5f;
        [field:SerializeField] public float ClimbingEnduranceMaximum { get; private set; } = 100f;
    }
}
