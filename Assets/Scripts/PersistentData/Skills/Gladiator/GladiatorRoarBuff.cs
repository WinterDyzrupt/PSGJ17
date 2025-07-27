using UnityEngine;

public class GladiatorRoarBuff : BuffDebuff
{
    public float damageBuffMultiplier = 2;
    public float armorDebuff = -5;
    public GameObject buffVFXPrefab;
    public GameObject buffVFX;

    protected override void ApplyBuffDebuff()
    {
        combatantController.currentCombatant.bonusOutgoingDamageMultiplier *= damageBuffMultiplier;
        combatantController.currentCombatant.bonusFlatDamageReduction += armorDebuff;
        buffVFX = Instantiate(buffVFXPrefab, transform);
    }

    protected override void UninstallBuffDebuff()
    {
        combatantController.currentCombatant.bonusOutgoingDamageMultiplier *= 1.0f / damageBuffMultiplier;
        combatantController.currentCombatant.bonusFlatDamageReduction -= armorDebuff;
        Destroy(buffVFX);
        base.UninstallBuffDebuff();
    }
}
