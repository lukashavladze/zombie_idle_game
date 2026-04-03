using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public PlayerStats playerStats;
    public List<PlayerUpgrade> allUpgrades;

    [Header("UI")]
    public GameObject upgradePanel;
    public UpgradeCardUI[] cards; // 3 cards

    private void Awake()
    {
        Instance = this;
    }

    public void ShowUpgrades()
    {
        Time.timeScale = 0f;

        upgradePanel.SetActive(true);

        List<PlayerUpgrade> selected = GetRandomUpgrades(3);

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Setup(selected[i]);
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShowUpgrades();
        }
    }

    public void SelectUpgrade(PlayerUpgrade upgrade)
    {
        playerStats.ApplyUpgrade(upgrade);

        upgradePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    List<PlayerUpgrade> GetRandomUpgrades(int count)
    {
        List<PlayerUpgrade> result = new List<PlayerUpgrade>();

        for (int i = 0; i < count; i++)
        {
            PlayerUpgrade random = allUpgrades[Random.Range(0, allUpgrades.Count)];
            result.Add(random);
        }

        return result;
    }
}