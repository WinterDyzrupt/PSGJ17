using System.Diagnostics;
using PersistentData;
using UnityEngine;

public class Skill : ScriptableObject
{
    // Base class for a Skill
    // Scriptable object so that we can created multiple skills with the base framework

    // Typical variables that will be needed for every skill
    public string displayName;
    public string description;
    public float cooldownTime;
    public int basePower;
    public Stopwatch cooldownTimeStopwatch;
    // Stretch Goals
    // public float windupTime; // in milliseconds
    // public Stopwatch windupTimeStopwatch;
    // public float recoveryTime; // in milliseconds
    // public Stopwatch recoveryTimeStopwatch;

    // Where the skills will do something
    // It needs to know who fired it.
    public virtual void ExecuteSkill(Combatant combatant) { }
}
