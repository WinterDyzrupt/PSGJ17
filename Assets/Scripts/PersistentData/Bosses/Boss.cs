using UnityEngine;

namespace PersistentData.Bosses
{
    [CreateAssetMenu(fileName = "Boss", menuName = "Combatants/Bosses/Boss")]
    public class Boss : Combatant
    {
        public int numberOfPhases;
        public int fameValue;

        public override void SetValues(Combatant combatantToGetValuesFrom)
        {
            base.SetValues(combatantToGetValuesFrom);
            var otherBoss = combatantToGetValuesFrom as Boss;
            Debug.Assert(otherBoss != null, nameof(combatantToGetValuesFrom) + " was expected to be a boss.");
            numberOfPhases = otherBoss.numberOfPhases;
        }

        public override void ResetValues()
        {
            base.ResetValues();
            numberOfPhases = 0;
        }
    }
}