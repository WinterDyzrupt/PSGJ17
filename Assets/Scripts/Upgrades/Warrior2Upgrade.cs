using PersistentData;
using UnityEngine;

//[CreateAssetMenu(fileName = "Warrior2", menuName = "Scriptable Objects/Upgrades/Warrior2")]
public class Warrior2Upgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}