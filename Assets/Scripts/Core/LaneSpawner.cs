using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Header("Lane X Ranges")]
    public Vector2 lane1Range = new Vector2(-18f, -10f);
    public Vector2 lane2Range = new Vector2(-10f, 10f);
    public Vector2 lane3Range = new Vector2(10f, 18f);

    [Header("Spawn Settings")]
    public float spawnZ = 20f;     // Z where zombies spawn
    public float targetZ = -20f;   // Z they walk to
    public GameObject zombiePrefab;
    public float spawnInterval = 2f;

    private float timer = 0f;

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
        float xPos = 0f;

        switch (lane)
        {
            case 1:
                xPos = Random.Range(lane1Range.x, lane1Range.y);
                break;
            case 2:
                xPos = Random.Range(lane2Range.x, lane2Range.y);
                break;
            case 3:
                xPos = Random.Range(lane3Range.x, lane3Range.y);
                break;
        }

        // Spawn high on Y so raycast always hits ground
        Vector3 spawnPos = new Vector3(xPos, 10f, spawnZ);

        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        SnapZombieToGround(zombie);

        // IMPORTANT: target must use the SAME grounded Y
        Vector3 targetPos = new Vector3(xPos, zombie.transform.position.y, targetZ);
        zombie.GetComponent<Zombie>().Init(targetPos);
    }

    void SnapZombieToGround(GameObject zombie)
    {
        CapsuleCollider col = zombie.GetComponent<CapsuleCollider>();
        if (!col) return;

        RaycastHit hit;
        if (Physics.Raycast(zombie.transform.position, Vector3.down, out hit, 50f))
        {
            float footOffset = (col.height * 0.5f) - col.center.y;
            zombie.transform.position = hit.point + Vector3.up * footOffset;
        }
    }
}
