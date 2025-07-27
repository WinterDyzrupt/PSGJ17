using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Image icon;
    public Upgrade upgrade;
    public UpgradeSelection currentUpgradeSelection;
    public UnityEvent upgradeSelectedEvent;

    public void SetAsSelected()
    {
        Debug.Assert(upgrade != null, nameof(upgrade) + " expected to be non-null.");
        Debug.Assert(currentUpgradeSelection != null, nameof(currentUpgradeSelection) + " expected to be non-null.");
        
        Debug.Log($"Setting {upgrade} as selected");
        currentUpgradeSelection.upgrade = upgrade;
        upgradeSelectedEvent?.Invoke();
    }
    public void UpdateButton()
    {
        Debug.Assert(icon != null, nameof(icon) + " expected to be non-null.");
        Debug.Assert(upgrade != null, upgrade.upgradeName + " expected to be non-null.");
        Debug.Assert(upgrade.rankIcons != null, upgrade.upgradeName + " rank icons expected to be non-null.");
        if ((upgrade?.rankIcons?.Length ?? 0) < upgrade.currentRank)
        {
            Debug.LogError($"Insufficient images in button upgrade for {upgrade.upgradeName}");
        }
        else
        {
            icon.sprite = upgrade.rankIcons[upgrade.currentRank];
        }
    }
}