using System;
using System.Collections;
using System.Collections.Generic;
using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skills/Skill")]
public class Skill : ScriptableObject
{
    public string displayName;
    public float baseTotalCooldownTimeInSeconds;
    public bool executePartsSerially;
    public bool isRanged;
    public List<SkillPart> skillParts;

    [NonSerialized]
    private float _lastExecutedTime = 0f;

    /// <summary>
    /// Multiplier may be changed at runtime, possibly by upgrades.
    /// </summary>
    [NonSerialized]
    private float _totalCooldownTimeMultiplier = DefaultCombatData.DefaultMultiplier;

    public bool IsOffCooldown
    {
        get
        {
            return CooldownPercentage >= 1;
        }
    }

    public float CooldownPercentage
    {
        get
        {
            if (_lastExecutedTime != 0)
            {
                float percentage = (Time.time - _lastExecutedTime) / TotalCooldownTime;

                if (percentage < 1)
                {
                    return percentage;
                }
            }
            return 1;
        }
    }

    public float TotalCooldownTime => baseTotalCooldownTimeInSeconds * _totalCooldownTimeMultiplier;
    
    public IEnumerator ExecuteSkillAsync(Transform transformParent, FactionType faction, float cooldownMultiplier = DefaultCombatData.DefaultMultiplier, float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        //Debug.Log($"Total cooldown: {baseTotalCooldownTimeInSeconds}; cooldown multiplier: {_totalCooldownTimeMultiplier} and cooldown percentage: {CooldownPercentage}.");
        // TODO: Refactor so that Update checks don't call for an execute if !isOffCooldown. (There might be a way in unity input system.) 
        if (IsOffCooldown)
        {
            _totalCooldownTimeMultiplier = cooldownMultiplier;
            _lastExecutedTime = Time.time;
            if ((skillParts?.Count ?? 0) == 0)
            {
                Debug.LogError($"{displayName}'s Skill doesn't have a list of parts.");
            }
            else
            {
                var target = FindTarget();
                var targetPosition = FindTargetPosition(target);
                var targetRotation = FindTargetRotation(target);
                foreach (var skillPart in skillParts)
                {
                    Debug.Log($"Executing part is {skillPart.name}.");
                    // this needs to be the correct transform from where it's spawned.
                    if (executePartsSerially)
                    {
                        yield return new WaitForSeconds(skillPart.windupTimeInSeconds);
                    }

                    if (target == null)
                    {
                        Debug.Log("Executing skill with no specific target.");
                        skillPart.ExecuteSkill(transformParent, faction, damageMultiplier);
                    }
                    else
                    {
                        Debug.Log("Executing skill with a target.");
                        skillPart.ExecuteSkill(transformParent, faction, damageMultiplier, targetPosition, targetRotation);
                    }

                    if (executePartsSerially)
                    {
                        yield return new WaitForSeconds(skillPart.recoveryTimeInSeconds);
                    }
                }
            }
        }
    }

    protected virtual GameObject FindTarget()
    {
        return null;
    }

    protected virtual Vector3 FindTargetPosition(GameObject target)
    {
        return target != null
            ? new Vector3(
                target.transform.position.x,
                target.transform.position.y,
                target.transform.position.z)
            : Vector3.zero;
    }

    protected virtual Quaternion FindTargetRotation(GameObject target)
    {
        return target != null
            ? new Quaternion(
                target.transform.rotation.x,
                target.transform.rotation.y,
                target.transform.rotation.z,
                target.transform.rotation.w)
            : Quaternion.identity;
    }
}
