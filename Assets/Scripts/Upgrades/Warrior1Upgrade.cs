using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Warrior1", menuName = "Scriptable Objects/Upgrade/Warrior1")]
public class Warrior1Upgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
