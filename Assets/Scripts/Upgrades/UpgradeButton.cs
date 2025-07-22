using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Image Icon;
    public Upgrade upgrade;

    public void UpdateButton()
    {
        if ((upgrade?.rankIcons?.Length ?? 0) < upgrade.currentRank)
        {
            Debug.LogError($"Insufficiant images in button upgrade for {upgrade.upgradeName}");
        }
        else
        {
            Icon.sprite = upgrade.rankIcons[upgrade.currentRank];
        }
    }
}