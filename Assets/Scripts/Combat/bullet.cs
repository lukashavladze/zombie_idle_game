using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 1;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {
        Debug.Log("BULLET SPAWNED: " + gameObject.name);

        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION WITH: " + collision.gameObject.name);

        Zombie zombie = collision.gameObject.GetComponentInParent<Zombie>();
        if (zombie != null)
        {
            Debug.Log("ZOMBIE HIT");
            zombie.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
