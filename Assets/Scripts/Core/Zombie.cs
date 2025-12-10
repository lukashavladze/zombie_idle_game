using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Vector3 target;
    public float speed = 3f;

    private Animator anim;

    public void Init(Vector3 targetPos)
    {
        target = targetPos;
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("IsWalking", true);

        Debug.Log("Animator found: " + anim);
        Debug.Log("IsWalking (Start) = " + anim.GetBool("IsWalking"));
    }

    void Update()
    {
        // ---- Fix rotation so zombie faces target but stays upright ----

        Vector3 dir = target - transform.position;
        dir.y = 0;                // IMPORTANT: Remove vertical rotation!
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        // ---- Move toward target ----

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"));
        //Debug.Log("STATE: " + anim.GetCurrentAnimatorStateInfo(0).fullPathHash);
        //Debug.Log("STATE NAME = " + anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);




        if (Vector3.Distance(transform.position, target) < 0.2f)
            Destroy(gameObject);
    }
}
