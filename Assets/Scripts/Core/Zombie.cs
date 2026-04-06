using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Data")]
    public ZombieData data;   // ASSIGN ON PREFAB

    [Header("Movement")]
    private Vector3 target;
    private float speed;

    [Header("Health")]
    private float maxHP;
    private float currentHP;

    private Animator anim;
    private bool isDead;
    private Rigidbody rb;
    private Collider col;
    public HealthBar healthBar;

    [Header("UI")]
    public GameObject damagePopupPrefab;
    public Transform damagePoint;
    public bool isBoss = false;

    // Called by spawner
    public void Init(Vector3 targetPos)
    {
        target = targetPos;
        float diff = GameManager.Instance.difficultyMultiplier;

        // ✅ stats come from ScriptableObject
        maxHP = data.baseHP * diff;
        currentHP = maxHP;
        speed = data.baseSpeed * diff;
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        anim.SetBool("IsWalking", true);
        if (healthBar != null)
            healthBar.gameObject.SetActive(isBoss);
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
            ReachBase();
    }

    // 🔥 DAMAGE ENTRY POINT
    public void TakeDamage(float dmg)
    {
        if (isDead) return;

        currentHP -= dmg;

        ShowDamage(dmg);
        anim.SetTrigger("Hit");
        if (healthBar != null && isBoss)
            healthBar.SetHealth(currentHP, maxHP);

        if (currentHP <= 0)
            Die();
    }

    void ShowDamage(float dmg)
    {
        if (!damagePopupPrefab || !damagePoint) return;

        GameObject popup = Instantiate(
            damagePopupPrefab,
            damagePoint.position,
            Quaternion.identity
        );

        popup.GetComponent<DamagePopup>().SetDamage(dmg);
    }

    void Die()
    {
        isDead = true;

        anim.SetBool("IsWalking", false);
        anim.SetTrigger("Die");

        if (rb)
            rb.isKinematic = true;

        if (col)
            col.enabled = false;

        // TODO: give coins → GameManager.AddCoins(data.rewardCoins);
        XPManager.Instance.AddXP(1);

        Destroy(gameObject, 1f);
    }

    void ReachBase()
    {
        // TODO: damage base here
        Destroy(gameObject);
    }
}
