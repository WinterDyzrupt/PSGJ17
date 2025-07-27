using PersistentData;
using UnityEngine;

[CreateAssetMenu(fileName = "Defense", menuName = "Scriptable Objects/Upgrades/Defense")]
public class DefenseUpgrade : Upgrade
{
    public float startingFlatDamageReduction = 1f;
    public float flatDamageReductionPerRank = .5f; 

    protected override void ApplyUpgrade(Combatant combatant, int rank)
    {
        base.ApplyUpgrade(combatant, rank);

        combatant.bonusFlatDamageReduction = startingFlatDamageReduction + flatDamageReductionPerRank * rank;
    }
}
