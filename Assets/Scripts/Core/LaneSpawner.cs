using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Header("Lane X Ranges")]
    public Vector2 lane1Range = new Vector2(-18f, -10f);
    public Vector2 lane2Range = new Vector2(-10f, 10f);
    public Vector2 lane3Range = new Vector2(10f, 18f);

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
        int lane = Random.Range(1, 4);
        float xPos = GetLaneX(lane);

        Vector3 spawnPos = new Vector3(xPos, 10f, spawnZ);

        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        SnapZombieToGround(zombie);

        Vector3 targetPos = new Vector3(xPos, zombie.transform.position.y, targetZ);
        zombie.GetComponent<Zombie>().Init(targetPos);
    }


    bool IsSpawnPointFree(GameObject zombie, Vector3 pos)
    {
        CapsuleCollider col = zombie.GetComponent<CapsuleCollider>();
        if (!col) return true;

        Vector3 center = pos + col.center;
        float radius = col.radius * 0.95f;

        Vector3 p1 = center + Vector3.up * (col.height / 2f - radius);
        Vector3 p2 = center - Vector3.up * (col.height / 2f - radius);

        Collider[] hits = Physics.OverlapCapsule(
            p1,
            p2,
            radius,
            zombieLayer
        );

        return hits.Length == 0;
    }

    float GetLaneX(int lane)
    {
        switch (lane)
        {
            case 1: return Random.Range(lane1Range.x, lane1Range.y);
            case 2: return Random.Range(lane2Range.x, lane2Range.y);
            case 3: return Random.Range(lane3Range.x, lane3Range.y);
        }
        return 0f;
    }

    void SnapZombieToGround(GameObject zombie)
    {
        CapsuleCollider col = zombie.GetComponent<CapsuleCollider>();
        if (!col) return;

        int groundMask = LayerMask.GetMask("Ground");

        Vector3 rayStart = zombie.transform.position + Vector3.up * 5f;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 20f, groundMask))
        {
            float footOffset = (col.height * 0.5f) - col.center.y;
            zombie.transform.position = hit.point + Vector3.up * footOffset;
        }
    }

}
