using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Scriptable Objects/Upgrades/Health")]
public class HealthUpgrade : Upgrade
{
    public int healthPerRank = 10;

    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        Debug.Log($"Applying upgrade: {base.ToString()} to combatant: {combatant}");
        combatant.bonusMaxHealth = rank * healthPerRank;
    }
}
