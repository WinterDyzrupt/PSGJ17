using System;

public class GladiatorUtilityBuff : StatusEffect
{
    public float armorDebuff = 100;
    public float MovementSpeedMultiplierDebuff = 0.1f;

    protected override void ApplyStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusFlatDamageReduction += armorDebuff;
            combatant.bonusMovementSpeedMultiplier *= MovementSpeedMultiplierDebuff;

            base.ApplyStatusEffect();
        }
    }

    public override void RemoveStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusFlatDamageReduction -= armorDebuff;
            combatant.bonusMovementSpeedMultiplier /= MovementSpeedMultiplierDebuff;

            base.RemoveStatusEffect();
        }
    }

    public override Type GetClass()
    {
        return GetType();
    }
}
