using PersistentData;
using UnityEngine;


[CreateAssetMenu(fileName = "Agility", menuName = "Scriptable Objects/Upgrades/Agility")]
public class AgilityUpgrade : Upgrade
{
    public float startingCooldownMultiplier = .1f;
    public float cooldownReductionMultiplierPerRank = .05f;

    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        base.ApplyUpgrade(combatant, rank);

        combatant.bonusCooldownReductionMultiplier = DefaultCombatData.DefaultMultiplier -
            startingCooldownMultiplier + cooldownReductionMultiplierPerRank * rank;
    }
}
