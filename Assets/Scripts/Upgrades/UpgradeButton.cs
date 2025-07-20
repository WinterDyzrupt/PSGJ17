using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Image Icon;
    public Sprite[] rankIcons;
    [SerializeField] private UpgradeSO upgradeSO;

    public void UpdateButton()
    {
        UpgradeProgress upgradeProgress = PlayerUpgrades.Instance.GetUpgradeProgress(upgradeSO);
        Icon.sprite = rankIcons[upgradeProgress.currentRank];
    }

    public void SendSelection()
    {
        ManagerUpgrade.Instance.SelectUpgrade(PlayerUpgrades.Instance.GetUpgradeProgress(upgradeSO));
    }
}