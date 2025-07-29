public class GladiatorRoarBuff : StatusEffect
{
    public float damageBuffMultiplier = 2;
    public float armorDebuff = -5;

    protected override void ApplyStatusEffect()
    {
        combatant.bonusOutgoingDamageMultiplier *= damageBuffMultiplier;
        combatant.bonusFlatDamageReduction += armorDebuff;

        base.ApplyStatusEffect();
    }

    public override void RemoveStatusEffect()
    {
        combatant.bonusOutgoingDamageMultiplier /= damageBuffMultiplier;
        combatant.bonusFlatDamageReduction -= armorDebuff;

        base.RemoveStatusEffect();
    }
}
