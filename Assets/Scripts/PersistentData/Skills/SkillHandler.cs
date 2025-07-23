using System.Collections.Generic;
using UnityEngine;
using PersistentData;

public class SkillHandler : MonoBehaviour
{
    public List<Skill> skills;
    
    public void ExecuteAllSkills(Combatant combatant)
    {
        if ((skills?.Count ?? 0) == 0)
        {
            Debug.LogError($"{gameObject.name}'s SkillHandler doesn't have a list of skills.");
        }

        // Maybe instead of this, we use a Queue and grab the current cooldown and run it through an Update?
        // this might be better to exist somewhere else though.
        foreach (Skill skill in skills)
        {
            skill.ExecuteSkill(combatant);
        }
    }
}