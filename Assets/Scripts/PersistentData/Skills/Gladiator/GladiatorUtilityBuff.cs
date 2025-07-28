public class GladiatorUtilityBuff : StatusEffect
{
    public float armorDebuff = 100;
    public float MovementSpeedMultiplierDebuff = 0.1f;

    protected override void ApplyStatusEffect()
    {
        combatant.bonusFlatDamageReduction += armorDebuff;
        combatant.bonusMovementSpeedMultiplier *= MovementSpeedMultiplierDebuff;

        base.ApplyStatusEffect();
    }

    public override void RemoveStatusEffect()
    {
        combatant.bonusFlatDamageReduction -= armorDebuff;
        combatant.bonusMovementSpeedMultiplier /= MovementSpeedMultiplierDebuff;

        base.RemoveStatusEffect();
    }
}
