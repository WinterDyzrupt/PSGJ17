using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skills/Skill")]
public class Skill : ScriptableObject
{
    public string displayName;
    public List<SkillPart> skillParts;

    [NonSerialized]
    private float _lastExecutedTime = 0f;

    [NonSerialized]
    private float _totalCooldownTime = 0f;

    [NonSerialized]
    private bool _isThisInitialized = false;

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
                float percentage = (Time.time - _lastExecutedTime) / _totalCooldownTime;

                if (percentage < 1)
                {
                    return percentage;
                }
            }
            return 1;
        }
    }

    public void DetermineTotalCooldownTime()
    {
        foreach (SkillPart part in skillParts)
        {
            _totalCooldownTime += part.cooldownTime;
        }
    }

    public void ExecuteSkill(Transform transformParent, FactionType faction)
    {
        if (!_isThisInitialized)
        {
            DetermineTotalCooldownTime();
            _isThisInitialized = true;
        }

        Debug.Log($"Total cooldown: {_totalCooldownTime} and cooldown percentage: {CooldownPercentage}.");
        if (isOffCooldown)
        {
            _lastExecutedTime = Time.time;

            if ((skillParts?.Count ?? 0) == 0)
            {
                Debug.LogError($"{displayName}'s Skill doesn't have a list of parts.");
            }



            foreach (SkillPart skillPart in skillParts)
            {
                Debug.Log($"Executing part is {skillPart.name}.");
                // this needs to be the correct transform from where it's spawned.
                skillPart.ExecuteSkill(transformParent, faction);
            }
        }
    }
}