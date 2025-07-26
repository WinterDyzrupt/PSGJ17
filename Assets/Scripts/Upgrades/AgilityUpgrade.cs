using PersistentData;
using UnityEngine;


[CreateAssetMenu(fileName = "Agility", menuName = "Scriptable Objects/Upgrades/Agility")]
public class AgilityUpgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}
