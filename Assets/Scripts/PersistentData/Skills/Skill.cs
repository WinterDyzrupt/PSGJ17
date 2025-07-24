using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skills/Skill")]
public class Skill : ScriptableObject
{
    public string displayName;
    public List<SkillPart> skillParts;

    public void ExecuteSkill(Transform transform)
    {
        if ((skillParts?.Count ?? 0) == 0)
        {
            Debug.LogError($"{displayName}'s Skill doesn't have a list of parts.");
        }

        foreach (SkillPart skillPart in skillParts)
        {
            // this needs to be the correct transform from where it's spawned.
            skillPart.ExecuteSkill(transform);
        }
    }
}