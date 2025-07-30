using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PersistentData.Skills.BossAttacks
{
    [CreateAssetMenu(fileName = "SkillGroup", menuName = "Scriptable Objects/Skills/SkillGroup")]
    public class SkillSetForPhase : ScriptableObject
    {
        public int phaseNumber = 1;
        public List<Skill> skillSet;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Skills for {nameof(phaseNumber)} {phaseNumber}:");
            if (skillSet != null)
            {
                foreach (var skill in skillSet)
                {
                    stringBuilder.AppendLine(skill?.displayName);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
