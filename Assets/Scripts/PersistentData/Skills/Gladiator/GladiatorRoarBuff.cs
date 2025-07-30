using System;

public class GladiatorRoarBuff : StatusEffect
{
    public float damageBuffMultiplier = 2;
    public float armorDebuff = -5;

    protected override void ApplyStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusOutgoingDamageMultiplier *= damageBuffMultiplier;
            combatant.bonusFlatDamageReduction += armorDebuff;

            base.ApplyStatusEffect();
        }
    }

    public override void RemoveStatusEffect()
    {
        if (combatant != null)
        {
            combatant.bonusOutgoingDamageMultiplier /= damageBuffMultiplier;
            combatant.bonusFlatDamageReduction -= armorDebuff;

            base.RemoveStatusEffect();
        }
    }

    public override Type GetClass()
    {
        return GetType();
    }
}
