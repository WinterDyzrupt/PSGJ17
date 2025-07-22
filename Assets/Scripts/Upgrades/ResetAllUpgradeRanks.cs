using PersistentData;
using UnityEngine;

public class ResetAllUpgradeRanks : MonoBehaviour
{
    public AllUpgrades allUpgrades;
    public AllWarriors allWarriors;

    public void SetAllRanksToZero()
    {
        foreach (Upgrade upgrade in allUpgrades)
        {
            upgrade.SetRank(0, allWarriors);
        }
    }
}
