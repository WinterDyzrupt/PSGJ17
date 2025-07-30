using System.Collections.Generic;
using PersistentData.Skills.BossAttacks;
using UnityEngine;

namespace PersistentData.Bosses
{
    [CreateAssetMenu(fileName = "Boss", menuName = "Combatants/Bosses/Boss")]
    public class Boss : Combatant
    {
        public int numberOfPhases;
        public List<SkillSetForPhase> allSkillsForAllPhases = new();
        public int fameValue;

        public override void SetValues(Combatant combatantToGetValuesFrom)
        {
            base.SetValues(combatantToGetValuesFrom);
            var otherBoss = combatantToGetValuesFrom as Boss;
            Debug.Assert(otherBoss != null, nameof(combatantToGetValuesFrom) + " was expected to be a boss.");
            numberOfPhases = otherBoss.numberOfPhases;
            allSkillsForAllPhases =  otherBoss.allSkillsForAllPhases;
        }

        public override void ResetValues()
        {
            base.ResetValues();
            numberOfPhases = 0;
            allSkillsForAllPhases.Clear();
        }
    }
}