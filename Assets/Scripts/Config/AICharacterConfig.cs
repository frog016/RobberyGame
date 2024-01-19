using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Data/Config/AI Character", fileName = "AICharacterConfig")]
    public class AICharacterConfig : CharacterConfig
    {
        [field: Header("AI")]
        [field: SerializeField] public float AttackDistance { get; private set; }
    }
}