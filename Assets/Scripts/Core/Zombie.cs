using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Vector3 target;
    public float speed = 3f;

    public void Init(Vector3 targetPos)
    {
        target = targetPos;
    }

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isWalking", true);
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.2f)
            Destroy(gameObject);
    }
}
