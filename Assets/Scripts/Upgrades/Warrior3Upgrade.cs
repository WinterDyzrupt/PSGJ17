using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Warrior3", menuName = "Scriptable Objects/Upgrade/Warrior3")]
public class Warrior3Upgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
