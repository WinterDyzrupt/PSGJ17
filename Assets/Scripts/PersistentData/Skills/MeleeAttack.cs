using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Skills/MeleeAttack")]
public class MeleeAttack : Skill
{
    // Skill Class for a Melee Attack
    public int power;
    // Prefab object that we can use to check for enemies in the area
    // Maybe there's a better way to check for this
    // public SkillShape meleeShape;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Combatant combatant)
    {
        // generate area to check for collisions

        // for each collision, that could be a hit, verify that it's an enemy combatant

        // Deal damage to that enemy
    }
}
