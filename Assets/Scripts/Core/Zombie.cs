using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;

    public void Init(Transform endPoint)
    {
        target = endPoint;
    }

    void Update()
    {
        if (target == null) return;

        // Move forward
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Reached the end
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject); // or damage player
        }
    }
}
