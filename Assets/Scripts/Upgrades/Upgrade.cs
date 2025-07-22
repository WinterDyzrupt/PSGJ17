using PersistentData.Warriors;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    [TextArea]
    public string upgradeName;
    [TextArea]
    public string[] upgradeDescription;
    public Sprite[] rankIcons;
    public int[] fameCostPerRank;
    public int maxRank;
    public bool IsMaxed => currentRank >= maxRank;
    public int currentRank;

    public int GetCostForNextRank()
    {
        int rankCost = 0;

        if (IsMaxed || (fameCostPerRank?.Length ?? 0) < currentRank)
        {
            Debug.LogError($"fameCostPerRank doesn't have enough entries in {upgradeName}.");
        }
        else
        {
            rankCost = fameCostPerRank[currentRank];
        }

        return rankCost;
    }

    public bool CanAffordNextRank(int currentFame)
    {
        return !IsMaxed && currentFame >= GetCostForNextRank();
    }

    public void IncreaseRank(AllWarriors allWarriors)
    {
        if (!IsMaxed)
        {
            currentRank++;
            ApplyToAllWarriors(allWarriors);
        }
        else
        {
            Debug.LogError("Attempted to increase Rank but it was already maxed.");
        }
    }

    public void SetRank(int rank, AllWarriors allWarriors)
    {
        if (rank <= maxRank)
        {
            currentRank = rank;
            ApplyToAllWarriors(allWarriors);
        }
        else
        {
            Debug.LogError("Attempted to set Rank beyond max.");
        }
    }


    public void ApplyToAllWarriors(AllWarriors allWarriors)
    {
        foreach (Warrior warrior in allWarriors.warriors) ApplyUpgrade(warrior, currentRank);
    }

    internal virtual void ApplyUpgrade(Warrior warrior, int rank) { }
}
