using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Evasion", menuName = "Scriptable Objects/Upgrades/Evasion")]
public class EvasionUpgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}