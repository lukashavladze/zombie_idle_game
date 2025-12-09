using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Header("Lane X Ranges")]
    public Vector2 lane1Range = new Vector2(-18f, -10f);
    public Vector2 lane2Range = new Vector2(-10f, 10f);
    public Vector2 lane3Range = new Vector2(10f, 18f);

    [Header("Spawn Settings")]
    public float spawnZ = 20f;           // Z where zombies spawn
    public float targetZ = -20f;         // Z they walk to
    public GameObject zombiePrefab;
    public float spawnInterval = 2f;

    public float spawnY = 2f; // HEIGHT ABOVE GROUND

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
        int lane = Random.Range(1, 4);   // 1,2,3
        float xPos = 0;

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

        // 👇 Zombie spawns above ground
        Vector3 spawnPos = new Vector3(xPos, spawnY, spawnZ);

        GameObject z = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        // 👇 Target is also above ground
        Vector3 targetPos = new Vector3(xPos, spawnY, targetZ);
        z.GetComponent<Zombie>().Init(targetPos);
    }
}
