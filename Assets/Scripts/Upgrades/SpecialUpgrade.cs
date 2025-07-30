using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Special", menuName = "Scriptable Objects/Upgrades/Special")]
public class SpecialUpgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}