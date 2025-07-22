using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Evasion", menuName = "Scriptable Objects/Upgrade/Evasion")]
public class EvasionUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
