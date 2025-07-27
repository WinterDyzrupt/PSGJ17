using PersistentData;
using UnityEngine;

public class ResetAllUpgradeRanks : MonoBehaviour
{
    public UpgradeGroup allUpgrades;
    public CombatantGroup allWarriors;

    public void SetAllRanks(int rank)
    {
        foreach (var upgrade in allUpgrades.upgrades)
        {
            upgrade.SetRank(rank, allWarriors);
        }
    }
}
