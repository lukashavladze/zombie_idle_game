using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Multipliers")]
    public float damageMult = 1f;
    public float fireRateMult = 1f;
    public float bulletSpeedMult = 1f;

    [Header("Additive Bonuses")]
    public int extraProjectiles = 0;

    [Header("Crit")]
    [Range(0f, 1f)]
    public float critChance = 0f;
    public float critMultiplier = 2f;

    // Called after skill selection
    public void ApplyUpgrade(PlayerUpgrade upgrade)
    {
        damageMult *= upgrade.damageMult;
        fireRateMult *= upgrade.fireRateMult;
        bulletSpeedMult *= upgrade.bulletSpeedMult;
        extraProjectiles += upgrade.extraProjectiles;

        critChance += upgrade.critChance;
        critMultiplier += upgrade.critMultiplier;
    }
}
