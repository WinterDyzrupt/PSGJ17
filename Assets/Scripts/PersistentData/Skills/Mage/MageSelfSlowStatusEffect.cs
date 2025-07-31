using System;

public class MageSelfSlowStatusEffect : StatusEffect
{
    public float MovementSpeedMultiplierDebuff = 0.1f;

    protected override void ApplyStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusMovementSpeedMultiplier *= MovementSpeedMultiplierDebuff;

            base.ApplyStatusEffect();
        }
    }

    public override void RemoveStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusMovementSpeedMultiplier /= MovementSpeedMultiplierDebuff;

            base.RemoveStatusEffect();
        }
    }

    public override Type GetClass()
    {
        return GetType();
    }
}
