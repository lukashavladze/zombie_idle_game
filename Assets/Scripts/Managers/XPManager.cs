using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance;

    [Header("XP")]
    public int level = 1;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;

    [Header("UI")]
    public Image xpFillImage;

    private void Awake()
    {
        Instance = this;
    }

    public void AddXP(float amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }

        UpdateUI();
    }

    void LevelUp()
    {
        level++;
        currentXP = 0;
        xpToNextLevel *= 1.5f;
        // need to test this
        UpdateUI();

        Debug.Log("LEVEL UP: " + level);

        UpgradeManager.Instance.ShowUpgrades(); // 👈 IMPORTANT
    }

    void UpdateUI()
    {
        if (xpFillImage != null)
        {
            xpFillImage.fillAmount = currentXP / xpToNextLevel;
        }
    }
}