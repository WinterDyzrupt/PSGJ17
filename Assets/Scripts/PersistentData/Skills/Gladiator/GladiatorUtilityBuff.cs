public class GladiatorUtilityBuff : BuffDebuff
{
    public float armorDebuff = 100;
    public float MovementSpeedMultiplierDebuff = 0.1f;

    protected override void ApplyBuffDebuff()
    {
        combatantController.currentCombatant.bonusFlatDamageReduction += armorDebuff;
        combatantController.currentCombatant.bonusMovementSpeedMultiplier *= MovementSpeedMultiplierDebuff;

        base.ApplyBuffDebuff();
    }

    protected override void UninstallBuffDebuff()
    {
        combatantController.currentCombatant.bonusFlatDamageReduction -= armorDebuff;
        combatantController.currentCombatant.bonusMovementSpeedMultiplier *= 1.0f / MovementSpeedMultiplierDebuff;

        base.UninstallBuffDebuff();
    }
}
