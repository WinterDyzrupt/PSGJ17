using Arena.Collisions;
using PersistentData;
using UnityEngine;

/// <summary>
/// Projectile attack.
/// </summary>
[CreateAssetMenu(fileName = "RangeAttack", menuName = "Scriptable Objects/Skills/Part-RangeAttack")]
public class RangeAttack : SkillPart
{    
    public GameObject projectileGameObject;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        if (projectileGameObject == null)
        {
            Debug.LogError($"{displayName} skill is missing its projectile GameObject.");
        }
        else
        {
            // Fire Projectile
            GameObject firedProjectile = Instantiate(projectileGameObject, transform);

            if (firedProjectile.TryGetComponent(out Projectile projectile))
            {
                projectile.InitializeProjectile(faction);
                projectile.gameObject.AddComponent<DealsDamageOnCollision>().damage =
                    new FloatReference(baseDamage * damageMultiplier);
            }
            else
            {
                Debug.LogError($"{displayName} failed to find a projectile in the GameObject.");
            }
        }
    }
}
