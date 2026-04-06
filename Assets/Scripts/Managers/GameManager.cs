using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Timer")]
    public float gameTime = 180f; // 3 minutes
    private float timer;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Difficulty")]
    public float difficultyMultiplier = 1f;

    private float difficultyTimer;
    private float bossTimer;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float remainingTime = gameTime - timer;

        // ⛔ GAME END
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            Debug.Log("GAME END");
            Time.timeScale = 0f;
        }

        UpdateTimerUI(remainingTime);

        // 🔥 Every 20 sec → increase difficulty
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= 20f)
        {
            difficultyMultiplier += 0.2f;
            difficultyTimer = 0f;
        }

        // 🔥 Every 50 sec → spawn boss
        bossTimer += Time.deltaTime;
        if (bossTimer >= 50f)
        {
            bossTimer = 0f;
            LaneSpawner.Instance.SpawnBoss();
        }
    }

    void UpdateTimerUI(float time)
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // 🔴 Last 10 seconds → red
        if (time <= 10f)
            timerText.color = Color.red;
    }
}