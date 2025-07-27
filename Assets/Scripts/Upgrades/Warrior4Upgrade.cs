using PersistentData;
using UnityEngine;

//[CreateAssetMenu(fileName = "Warrior4", menuName = "Scriptable Objects/Upgrades/Warrior4")]
public class Warrior4Upgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}
