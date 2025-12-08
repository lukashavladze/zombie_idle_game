using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public Transform[] laneStarts;   // Start points
    public Transform[] laneEnds;     // End points
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
        int lane = Random.Range(0, laneStarts.Length);

        Transform start = laneStarts[lane];
        Transform end = laneEnds[lane];

        GameObject z = Instantiate(zombiePrefab, start.position, Quaternion.identity);

        z.GetComponent<Zombie>().Init(end);
    }
}
