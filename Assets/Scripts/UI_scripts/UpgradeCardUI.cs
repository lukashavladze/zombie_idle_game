using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCardUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public Image icon;

    private PlayerUpgrade currentUpgrade;

    public void Setup(PlayerUpgrade upgrade)
    {
        currentUpgrade = upgrade;

        titleText.text = upgrade.title;
        descText.text = upgrade.description;
        icon.sprite = upgrade.icon;
    }

    public void OnClick()
    {
        UpgradeManager.Instance.SelectUpgrade(currentUpgrade);
    }
}