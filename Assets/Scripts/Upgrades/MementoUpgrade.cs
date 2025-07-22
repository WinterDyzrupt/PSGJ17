using PersistentData.Warriors;
using UnityEngine;

[CreateAssetMenu(fileName = "Memento", menuName = "Scriptable Objects/Upgrade/Memento")]
public class MementoUpgrade : Upgrade
{
        internal override void ApplyUpgrade(Warrior warrior, int rank)
        {
            // warrior.health = rank * 10 + warrior.baseHealth;
        }
}
