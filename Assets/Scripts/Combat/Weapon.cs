using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Firing Settings")]
    public float fireRate = 5f;  // bullets per second
    public int burstCount = 1;   // shots per tap
    public float bulletSpeed = 25f;
    public float bulletLifetime = 2f;

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
            GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Bullet bullet = b.GetComponent<Bullet>();
            bullet.speed = bulletSpeed;
            bullet.lifeTime = bulletLifetime;
        }
    }
}
