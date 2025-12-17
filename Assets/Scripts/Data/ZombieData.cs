using UnityEngine;

[CreateAssetMenu(menuName = "Data/Zombie")]
public class ZombieData : ScriptableObject
{
    public float baseHP = 10f;
    public float baseSpeed = 2f;
    public int rewardCoins = 1;
}
