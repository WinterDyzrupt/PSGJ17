using UnityEngine;

namespace PersistentData.Warriors
{
    [CreateAssetMenu(fileName = "Warrior", menuName = "Warriors/Warrior")]
    public class Warrior : Combatant
    {
        public Skill skill1BasicAttack;
        public Skill skill2Ability1;
        public Skill skill3Ability2;
        public Skill skill4Utility;

    }
}
