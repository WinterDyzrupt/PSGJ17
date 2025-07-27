using PersistentData;
using UnityEngine;

//[CreateAssetMenu(fileName = "Warrior3", menuName = "Scriptable Objects/Upgrades/Warrior3")]
public class Warrior3Upgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}