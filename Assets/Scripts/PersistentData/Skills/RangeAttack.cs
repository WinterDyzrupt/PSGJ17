using UnityEngine;

[CreateAssetMenu(fileName = "RangeAttack", menuName = "Scriptable Objects/Skills/Part-RangeAttack")]
public class RangeAttack : SkillPart
{
    // Skill Class for a Melee Attack
    
    public GameObject projectileGameObject;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Transform transform)
    {

        if (projectileGameObject == null)
        {
            Debug.LogError($"{displayName} skill is missing its projectile GameObject.");
            return;
        }

        // Fire Projectile
        GameObject firedProjectile = Instantiate(projectileGameObject, transform);

        if (firedProjectile.TryGetComponent(out Projectile projectile))
        {
            projectile.InitializeProjectile();
        }
        else
        {
            Debug.LogError($"{displayName} failed to find a projectile in the GameObject.");
        }
    }
}
