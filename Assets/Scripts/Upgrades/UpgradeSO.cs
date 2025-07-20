using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    [TextArea]
    public string upgradeName;
    [TextArea]
    public string[] upgradeDescription;
    public int maxRank;
    public int[] fameCostPerRank;
}