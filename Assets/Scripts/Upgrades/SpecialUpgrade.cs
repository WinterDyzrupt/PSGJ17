using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Special", menuName = "Scriptable Objects/Upgrade/Special")]
public class SpecialUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
