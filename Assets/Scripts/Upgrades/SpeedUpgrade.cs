using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed", menuName = "Scriptable Objects/Upgrades/Speed")]
public class SpeedUpgrade : Upgrade
{
    public float startingMovementSpeedMultiplier = DefaultCombatData.DefaultMultiplier;
    public float movementSpeedMultiplierPerRank = .1f;
    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        base.ApplyUpgrade(combatant, rank);

        combatant.bonusMovementSpeedMultiplier = startingMovementSpeedMultiplier + movementSpeedMultiplierPerRank * rank;
    }
}
