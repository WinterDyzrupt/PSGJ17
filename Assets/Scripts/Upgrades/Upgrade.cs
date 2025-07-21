using UnityEngine;

public class Upgrade : ScriptableObject
{
    [TextArea]
    public string upgradeName;
    [TextArea]
    public string[] upgradeDescription;
    public int currentRank;
    public int maxRank;
    public bool IsMaxed => currentRank >= maxRank;
    public int[] fameCostPerRank;

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

    public bool CanAffordNextRank()
    {
        return !IsMaxed && PlayerUpgrades.Instance.Fame >= GetCostForNextRank();
    }

    public void IncreaseRank()
    {
        if (!IsMaxed)
        {
            currentRank++;
        }
        else
        {
            Debug.LogError("Attempted to increase Rank but it was already maxed.");
        }
    }

    public void SetRank(int rank)
    {
        if (rank <= maxRank)
        {
            currentRank = rank;
        }
        else
        {
            Debug.LogError("Attempted to set Rank beyond max.");
        }
    }


    /*     public void ApplyToAllWarriors(AllWarriors allWarriors)
        {
            foreach (Warrior warrior in allWarriors) ApplyUpgrade(warrior, currentRank);
        }

        internal virtual void ApplyUpgrade(Warrior warrior, int rank) { } */
}

/* [CreateAssetMenu(menuName = "Upgrades/Health")]
public class Health : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            warrior.health = rank * 10 + warrior.baseHealth;
        }
} */