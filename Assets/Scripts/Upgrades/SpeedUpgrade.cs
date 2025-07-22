using PersistentData.Warriors;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed", menuName = "Scriptable Objects/Upgrade/Speed")]
public class SpeedUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
