using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Data")]
    public WeaponData weaponData;
    public BulletData bulletData;

    [Header("Refs")]
    public Transform firePoint;

    private float fireTimer;
    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();

        if (!playerStats)
            Debug.LogError("❌ PlayerStats NOT found on parent!", this);

        if (!weaponData)
            Debug.LogError("❌ WeaponData not assigned!", this);

        if (!bulletData)
            Debug.LogError("❌ BulletData not assigned!", this);

        if (!firePoint)
            Debug.LogError("❌ FirePoint not assigned!", this);
    }

    void Update()
    {
        if (!playerStats || !weaponData || !bulletData || !firePoint)
            return; // 🚑 prevents crash

        fireTimer += Time.deltaTime;

        float finalFireRate = weaponData.baseFireRate * playerStats.fireRateMult;

        if (fireTimer >= 1f / finalFireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    void Fire()
    {
        int projectiles =
            weaponData.baseProjectiles + playerStats.extraProjectiles;

        for (int i = 0; i < projectiles; i++)
        {
            float damage =
                weaponData.baseDamage *
                bulletData.damageMultiplier *
                playerStats.damageMult;

            // 🎯 Crit
            if (Random.value < playerStats.critChance)
                damage *= playerStats.critMultiplier;

            GameObject bullet = Instantiate(
                bulletData.prefab,
                firePoint.position,
                Quaternion.identity
            );

            bullet.GetComponent<Bullet>().Init(
                damage,
                weaponData.baseBulletSpeed * playerStats.bulletSpeedMult,
                weaponData.baseBulletLifetime
            );
        }
    }
}
