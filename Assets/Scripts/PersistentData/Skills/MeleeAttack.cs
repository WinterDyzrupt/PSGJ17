using System.Threading.Tasks;
using Arena.Collisions;
using PersistentData;
using Arena.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Skills/Part-MeleeAttack")]
public class MeleeAttack : SkillPart
{
    public float offsetFromTheSpawner = 35;
    public GameObject shapePrefab;
    public int framesToKeepEffectAlive = 1;

    // Override Execute skill so that it performs the attack
    public override Task ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        if (shapePrefab != null)
        {
            GameObject shape = Instantiate(shapePrefab, transform);
            shape.transform.localPosition += Vector3.right * offsetFromTheSpawner / transform.localScale.x;
            if (baseDamage > 0)
            {
                shape.AddComponent<DealsDamageOnCollision>().damage = new FloatReference(baseDamage * damageMultiplier);
            }
            shape.AddComponent<Faction>().faction = faction;
            shape.AddComponent<DestroyAfterElapsedFrames>().framesToKeepAlive = framesToKeepEffectAlive;
            shape.transform.SetParent(null);
            shape.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogError($"{displayName} doesn't have a prefab shape.");
        }

        return Task.CompletedTask;
    }
}
