using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Warrior4", menuName = "Scriptable Objects/Upgrade/Warrior4")]
public class Warrior4Upgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
