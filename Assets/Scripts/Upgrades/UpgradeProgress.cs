[System.Serializable]
public class UpgradeProgress
{
    public UpgradeSO upgrade;
    public int currentRank = 0;

    public bool IsMaxed => currentRank >= upgrade.maxRank;

    public int GetCostForNextRank()
    {
        if (IsMaxed) return int.MaxValue;
        return upgrade.fameCostPerRank[currentRank];
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
    }
}
