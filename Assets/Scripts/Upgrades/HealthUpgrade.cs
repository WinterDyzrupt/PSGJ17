using PersistentData.Warriors;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Scriptable Objects/Upgrade/Health")]
public class HealthUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
