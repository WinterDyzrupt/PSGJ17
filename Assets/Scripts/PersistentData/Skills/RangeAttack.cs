using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Scriptable Objects/Skills/RangeAttack")]
public class RangeAttack : Skill
{
    // Skill Class for a Melee Attack
    public int power;
    public GameObject projectileGameObject;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Combatant combatant)
    {

        if (projectileGameObject == null)
        {
            Debug.LogError($"{displayName} skill is missing its projectile GameObject.");
            return;
        }

        // Fire Projectile
        GameObject firedProjectile = Instantiate(projectileGameObject);

        // Grab the class that operate the projectile
        // Projectile projectile = firedProjectile.GetComponent<Projectile>();

        // give the projectile information about what it's doing and who made it
        // this should also allow it to offset from the combatant so that it doesn't spawn weirdly
        // projectile.Initialize(combatant, this);

        // make sure it's not attached to the combatant
        firedProjectile.transform.SetParent(null);
    }
}
