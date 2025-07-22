using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Agility", menuName = "Scriptable Objects/Upgrade/Agility")]
public class AgilityUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
