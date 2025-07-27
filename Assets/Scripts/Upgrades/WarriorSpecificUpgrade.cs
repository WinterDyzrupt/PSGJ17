using PersistentData;
using PersistentData.Warriors;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorSpecific", menuName = "Scriptable Objects/Upgrades/WarriorSpecific")]
public class WarriorSpecificUpgrade : Upgrade
{
    public Warrior warriorToUpgrade;

    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        //base.ApplyUpgrade(combatant, rank);
    }
}