using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Movement")]
    private Vector3 target;
    public float speed = 3f;

    [Header("Health")]
    public int maxHP = 3;
    private int currentHP;

    private Animator anim;
    private bool isDead = false;
    private Rigidbody rb;
    private Collider col;

    // Called by spawner
    public void Init(Vector3 targetPos)
    {
        target = targetPos;
    }

    void Start()
    {
        currentHP = maxHP;

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        anim.SetBool("IsWalking", true);
    }

    void Update()
    {
        if (isDead) return;

        Vector3 dir = target - transform.position;
        dir.y = 0f;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.2f)
            Destroy(gameObject);
    }

    // 🔥 DAMAGE ENTRY POINT
    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHP -= dmg;

        // play hit animation
        anim.SetTrigger("Hit");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        anim.SetBool("IsWalking", false);
        anim.SetTrigger("Die");

        // stop physics
        if (rb)
        {
            //rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // disable collider so bullets pass through
        if (col)
            col.enabled = false;

        // destroy after death animation
        Destroy(gameObject, 1.0f);
    }
}
