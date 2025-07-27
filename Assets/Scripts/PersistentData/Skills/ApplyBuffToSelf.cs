using System;
using UnityEditor;
using UnityEngine;

public class ApplyBuffToSelf : SkillPart
{
    public MonoScript buffDebuffChild;

    public override void ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier = 1)
    {
        Type buffClass = buffDebuffChild?.GetClass();
        transform.parent.gameObject.AddComponent(buffClass);
    }
}
