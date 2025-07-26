using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed", menuName = "Scriptable Objects/Upgrades/Speed")]
public class SpeedUpgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}
