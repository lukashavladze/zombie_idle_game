using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Base Stats")]
    public float baseDamage = 1f;
    public float baseFireRate = 5f;      // bullets per second
    public float baseBulletSpeed = 40f;
    public float baseBulletLifetime = 2f;

    [Header("Burst")]
    public int baseProjectiles = 1;
}
