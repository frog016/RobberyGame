using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Data/Config/Character", fileName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("Stat")]
        [field: SerializeField] public int MaxHealth { get; private set; }

        [field: Header("Movement")]
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ChargeSpeed { get; private set; }
        [field: SerializeField] public float SquatSpeed { get; private set; }

        [field: Header("State Duration")]
        [field: SerializeField] public float ChargeDuration { get; private set; }
        [field: SerializeField] public float ReloadDuration { get; private set; }
        [field: SerializeField] public float InteractDuration { get; private set; }
    }
}