using PersistentData.Warriors;
using UnityEngine;

[CreateAssetMenu(fileName = "Warrior0", menuName = "Scriptable Objects/Upgrade/Warrior0")]
public class Warrior0Upgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
