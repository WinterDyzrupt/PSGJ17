using System;
using UnityEngine;

public class MageSlowedStatusEffect : StatusEffect
{
    public float speedDebuff = 0.95f;

    protected override void ApplyStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusMovementSpeedMultiplier *= speedDebuff;

            base.ApplyStatusEffect();
        }
    }

    public override void RemoveStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusMovementSpeedMultiplier /= speedDebuff;

            base.RemoveStatusEffect();
        }
    }

    public override Type GetClass()
    {
        return GetType();
    }
}