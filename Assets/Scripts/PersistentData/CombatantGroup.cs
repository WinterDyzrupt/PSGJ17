using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PersistentData
{
    [CreateAssetMenu(fileName = "CombatantGroup", menuName = "Combatants/CombatantGroup")]
    public class CombatantGroup : ScriptableObject
    {
        public List<Combatant> combatants = new();
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Party size: " + (combatants?.Count ?? 0));
            if (combatants != null)
            {
                foreach (var warrior in combatants)
                {
                    stringBuilder.AppendLine("\tParty member: " + warrior);
                }
            }

            return stringBuilder.ToString();
        }
    }
}