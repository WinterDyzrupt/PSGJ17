using PersistentData;
using UnityEngine;

public class SkillPart : ScriptableObject
{
    // Part of a skill for operation
    // Scriptable object so that we can created multiple skill parts with the base framework

    // Typical variables that will be needed for every skill
    public string displayName;
    public string description;
    public float cooldownTime;
    public int basePower;
    // Stretch Goals
    // public float windupTime; // in seconds
    // public float recoveryTime; // in seconds

    // Where the skills will do something
    // It needs to know who fired it.
    public virtual void ExecuteSkill() { }
    public virtual void ExecuteSkill(Transform transform) { }
    public virtual void ExecuteSkill(Combatant combatant) { }
    // public virtual void ExecuteSkill(Faction faction) { }
}
