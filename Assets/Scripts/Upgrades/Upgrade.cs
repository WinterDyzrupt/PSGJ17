using PersistentData;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string[] upgradeDescription;
    public Sprite[] rankIcons;
    public int[] fameCostPerRank;
    public int maxRank;
    public bool IsMaxed => currentRank >= maxRank;
    public int currentRank;

    public int GetCostForNextRank()
    {
        int rankCost = 0;

        if ((fameCostPerRank?.Length ?? 0) < currentRank)
        {
            Debug.LogError($"fameCostPerRank doesn't have enough entries in {upgradeName}.");
        }
        else if (!IsMaxed)
        {
            rankCost = fameCostPerRank[currentRank];
        }

        return rankCost;
    }

    public bool CanAffordNextRank(int currentFame)
    {
        return !IsMaxed && currentFame >= GetCostForNextRank();
    }

    public void IncreaseRank(CombatantGroup group)
    {
        SetRank(currentRank + 1, group);
    }

    public void SetRank(int rank, CombatantGroup group)
    {
        if (rank <= maxRank)
        {
            currentRank = rank;
            ApplyToCombatantGroup(group);
        }
        else
        {
            Debug.LogError("Attempted to set Rank beyond max.");
        }
    }

    public void ApplyToCombatantGroup(CombatantGroup group)
    {
        Debug.Log($"Applying upgrade: {this.ToString()} to Combatants: {group}");
        foreach (var combatant in group.combatants)
        {
            ApplyUpgrade(combatant, currentRank);
        }
    }

    protected virtual void ApplyUpgrade(Combatant combatant, int rank)
    {
        Debug.Log($"Applying upgrade: {upgradeName}, rank: {rank} to combatant: {combatant}");
    }

    public override string ToString()
    {
        return $"Upgrade: {upgradeName}, currentRank: {currentRank}";
    }
}