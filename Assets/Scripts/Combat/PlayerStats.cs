using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float fireRateBoost = 1f;
    public float bulletSpeedBoost = 1f;
    public int extraProjectiles = 0;

    private Weapon weapon;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void ApplyUpgrades()
    {
        weapon.fireRate *= fireRateBoost;
        weapon.bulletSpeed *= bulletSpeedBoost;
        weapon.burstCount += extraProjectiles;
    }
}
