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

    public void Init(Vector3 targetPos)
    {
        target = targetPos;
    }

    void Start()
    {
        currentHP = maxHP;

        anim = GetComponentInChildren<Animator>();
        anim.SetBool("IsWalking", true);
    }

    void Update()
    {
        if (isDead) return;

        Vector3 dir = target - transform.position;
        dir.y = 0;

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

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        anim.SetTrigger("Die");

        // Optional delay to play animation
        Destroy(gameObject, 1.2f);
    }
}
