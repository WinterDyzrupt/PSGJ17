using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Warrior2", menuName = "Scriptable Objects/Upgrade/Warrior2")]
public class Warrior2Upgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
