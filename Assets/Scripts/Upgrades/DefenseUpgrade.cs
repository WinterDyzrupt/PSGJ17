using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Defense", menuName = "Scriptable Objects/Upgrades/Defense")]
public class DefenseUpgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}
