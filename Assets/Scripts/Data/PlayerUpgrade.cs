using UnityEngine;

[CreateAssetMenu(
    fileName = "New Player Upgrade",
    menuName = "Game/Player Upgrade"
)]
public class PlayerUpgrade : ScriptableObject
{
    [Header("Multipliers")]
    public float damageMult = 1f;
    public float fireRateMult = 1f;
    public float bulletSpeedMult = 1f;

    [Header("Additive")]
    public int extraProjectiles = 0;

    [Header("Crit")]
    [Range(0f, 1f)]
    public float critChance = 0f;
    public float critMultiplier = 0f;

    [Header("UI")]
    public string title;
    [TextArea] public string description;
    public Sprite icon;
}
