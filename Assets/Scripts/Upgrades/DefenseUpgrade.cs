using PersistentData.Warriors;
using UnityEngine;

[CreateAssetMenu(fileName = "Defense", menuName = "Scriptable Objects/Upgrade/Defense")]
public class DefenseUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
