using System.Threading.Tasks;
using Arena.Collisions;
using Arena.Combat;
using PersistentData;
using UnityEngine;

public abstract class SkillPart : ScriptableObject
{
    // Part of a skill for operation
    // Scriptable object so that we can create multiple skill parts with the base framework

    // Typical variables that will be needed for every skill
    public string displayName;
    public string description;
    public float baseDamage;
    public float windupTimeInSeconds;
    public float recoveryTimeInSeconds;

    // Where the skills will do something
    // It needs to know who fired it.
    public virtual Task ExecuteSkill(Transform transform, FactionType faction,
        float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        return Task.CompletedTask;
    }

    public virtual Task ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier, Vector3 targetPosition, Quaternion targetRotation)
    {
        return ExecuteSkill(transform, faction, damageMultiplier);
    }

    public virtual Task ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier, GameObject target)
    {
        return ExecuteSkill(transform, faction, damageMultiplier, target.transform.position, target.transform.rotation);
    }

    protected void ConfigureCreatedObject(GameObject createdObject, FactionType faction, float timeToKeepObjectAliveInSeconds, float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        if (baseDamage > 0)
        {
            createdObject.AddComponent<DealsDamageOnCollision>().damage = new FloatReference(baseDamage * damageMultiplier);
        }
        createdObject.AddComponent<Faction>().faction = faction;
        createdObject.AddComponent<DestroyAfterElapsedFrames>().secondsToKeepAlive = timeToKeepObjectAliveInSeconds;
        createdObject.transform.SetParent(null);
        createdObject.transform.localScale = Vector3.one;
    }
}
