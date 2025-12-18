using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Header("Lane X Ranges")]
    public Vector2 lane1Range = new Vector2(-18f, -10f);
    public Vector2 lane2Range = new Vector2(-10f, 10f);

    [Header("Spawn Settings")]
    public float spawnZ = 20f;
    public float targetZ = -20f;
    public GameObject zombiePrefab;
    public float spawnInterval = 0.1f;

    [Header("Collision Check")]
    public LayerMask zombieLayer;
    public float zombieRadius = 30f;
    public float zombieHeight = 50f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnZombie();
            timer = 0f;
        }
    }

    void SpawnZombie()
    {
        int lane = Random.Range(1, 3);
        float xPos = GetLaneX(lane);

        Vector3 spawnPos = new Vector3(xPos, 1f, spawnZ);
        Vector3 targetPos = new Vector3(xPos, 1f, targetZ);

        Vector3 dir = (targetPos - spawnPos).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        GameObject zombie = Instantiate(zombiePrefab, spawnPos, rot);

        SnapZombieToGround(zombie);

        zombie.GetComponent<Zombie>().Init(targetPos); // need to edit
    }

    float GetLaneX(int lane)
    {
        switch (lane)
        {
            case 1: return Random.Range(lane1Range.x, lane1Range.y);
            case 2: return Random.Range(lane2Range.x, lane2Range.y);
        }
        return 0f;
    }

    void SnapZombieToGround(GameObject zombie)
    {
        CapsuleCollider col = zombie.GetComponent<CapsuleCollider>();
        if (!col) return;

        int groundMask = LayerMask.GetMask("Ground");

        // start ray well above the zombie
        Vector3 rayStart = zombie.transform.position + Vector3.up * 5f;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 20f, groundMask))
        {
            // bottom of capsule in local space
            float bottom = col.center.y - (col.height * 0.5f);

            // move so capsule bottom touches ground
            zombie.transform.position += Vector3.up * (hit.point.y - (zombie.transform.position.y + bottom));
        }
    }

}
