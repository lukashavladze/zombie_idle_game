using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Firing Settings")]
    public float fireRate = 50f;  // bullets per second
    public int burstCount = 5;   // shots per tap
    public float bulletSpeed = 60f;
    public float bulletLifetime = 2f;
    public float bulletdamage_fromweapon;

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    public void Fire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Bullet bullet = b.GetComponent<Bullet>();
            bullet.speed = bulletSpeed;
            bullet.lifeTime = bulletLifetime;
            bullet.damage = bulletdamage_fromweapon;
        }
    }
}
