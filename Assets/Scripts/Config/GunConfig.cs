using Animation;
using Entity.Attack;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "Data/Config/Gun", fileName = "GunConfig")]
    public class GunConfig : ScriptableObject
    {
        [field: Header("Gun Data")]
        [field: SerializeField] public int ShootDamage { get; private set; }
        [field: SerializeField] public float ShootCooldown { get; private set; }
        [field: SerializeField] public float ShootSpread { get; private set; }
        [field: SerializeField] public int BulletLaunchCount { get; private set; }
        [field: SerializeField] public float BulletLaunchDelay { get; private set; }

        [field: Header("Magazine Data")]
        [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
        [field: SerializeField] public int MagazineCapacity { get; private set; }

        [field: Header("View Data")] 
        [field: SerializeField] public GunView ViewPrefab { get; private set; }
    }
}
