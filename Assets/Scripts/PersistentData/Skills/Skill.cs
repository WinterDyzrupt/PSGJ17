using System;
using System.Collections.Generic;
using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skills/Skill")]
public class Skill : ScriptableObject
{
    public string displayName;
    public float baseTotalCooldownTime;
    public List<SkillPart> skillParts;

    [NonSerialized]
    private float _lastExecutedTime = 0f;

    /// <summary>
    /// Multiplier may be changed at runtime, possibly by upgrades.
    /// </summary>
    [NonSerialized]
    private float _totalCooldownTimeMultiplier = DefaultCombatData.DefaultMultiplier;

    public bool isOffCooldown
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

    public float TotalCooldownTime => baseTotalCooldownTime * _totalCooldownTimeMultiplier;
    
    public void ExecuteSkill(Transform transformParent, FactionType faction, float cooldownMultiplier = DefaultCombatData.DefaultMultiplier, float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        // Debug.Log($"Total cooldown: {baseTotalCooldownTime}; cooldown multiplier: {_totalCooldownTimeMultiplier} and cooldown percentage: {CooldownPercentage}.");
        // TODO: Refactor so that Update checks don't call for an execute if !isOffCooldown. (There might be a way in unity input system.) 
        if (isOffCooldown)
        {
            _totalCooldownTimeMultiplier = cooldownMultiplier;
            _lastExecutedTime = Time.time;

            if ((skillParts?.Count ?? 0) == 0)
            {
                Debug.LogError($"{displayName}'s Skill doesn't have a list of parts.");
            }

            foreach (SkillPart skillPart in skillParts)
            {
                Debug.Log($"Executing part is {skillPart.name}.");
                // this needs to be the correct transform from where it's spawned.
                skillPart.ExecuteSkill(transformParent, faction, damageMultiplier: damageMultiplier);
            }
        }
    }
}