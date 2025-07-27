using UnityEngine;

namespace PersistentData.Warriors
{
    [CreateAssetMenu(fileName = "Warrior", menuName = "Combatants/Warriors/Warrior")]
    public class Warrior : Combatant
    {
        public Skill basicAttack;
        public Skill ability1;
        public Skill ability2;
        public Skill utility;

        public override void SetValues(Combatant combatantToGetValuesFrom)
        {
            base.SetValues(combatantToGetValuesFrom);

            if (combatantToGetValuesFrom is Warrior warriorToGetValuesFrom)
            {
                this.basicAttack = warriorToGetValuesFrom.basicAttack;
                this.ability1 = warriorToGetValuesFrom.ability1;
                this.ability2 = warriorToGetValuesFrom.ability2;
                this.utility = warriorToGetValuesFrom.utility;
            }
            else { Debug.LogError(combatantToGetValuesFrom + " was not a warrior."); }
        }

        public override void Reset()
        {
            base.Reset();

            this.basicAttack = null;
            this.ability1 = null;
            this.ability2 = null;
            this.utility = null;
        }
    }
}
