using UnityEngine;

[CreateAssetMenu(menuName = "Data/Bullet")]
public class BulletData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Stats")]
    public float damageMultiplier = 1f;
}
