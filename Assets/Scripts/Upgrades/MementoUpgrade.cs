using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Memento", menuName = "Scriptable Objects/Upgrades/Memento")]
public class MementoUpgrade : Upgrade
{
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        // warrior.health = rank * 10 + warrior.baseHealth;
    }
}
