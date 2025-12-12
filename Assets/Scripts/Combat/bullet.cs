using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 1;

    private float timer = 0f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            // Apply damage here
            // other.GetComponent<Zombie>().TakeDamage(damage);

            Destroy(gameObject); // bullet disappears on hit
        }
    }
}
