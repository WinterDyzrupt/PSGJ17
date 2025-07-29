using UnityEngine;

namespace PersistentData.Skills
{
    [CreateAssetMenu(fileName = "TargetedSkill", menuName = "Scriptable Objects/Skills/TargetedSkillAtPlayer")]
    public class TargetedSkillAtPlayer : Skill
    {
        protected override GameObject FindTarget()
        {
            return GameObject.FindWithTag(Tags.Player);
        }
    }
}
