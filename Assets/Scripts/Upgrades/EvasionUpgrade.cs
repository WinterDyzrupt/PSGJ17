using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Evasion", menuName = "Scriptable Objects/Upgrades/Evasion")]
public class EvasionUpgrade : Upgrade
{
    public int avoidAttackCountPerRank = 1;
    public float avoidAttackIntervalInSecondsPerRank;

    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        base.ApplyUpgrade(combatant, rank);

        combatant.bonusAvoidAttackCount = avoidAttackCountPerRank * rank;
        combatant.bonusAvoidAttackIntervalInSeconds = avoidAttackIntervalInSecondsPerRank * rank;
    }
}