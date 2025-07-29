using System.Threading.Tasks;
using UnityEngine;

namespace PersistentData.Skills
{
    [CreateAssetMenu(fileName = "TargetedRangedAttack", menuName = "Scriptable Objects/Skills/Part-TargetedRangedAttack")]
    public class TargetedRangedAttack : SkillPart
    {
        public GameObject attackPrefab;
        public float timeToKeepEffectAliveInSeconds = .25f;

        public override Task ExecuteSkill(Transform transform, FactionType faction,
            float damageMultiplier, Vector3 targetPosition, Quaternion targetRotation)
        {
            if (attackPrefab == null)
            {
                Debug.LogError($"{nameof(attackPrefab)} was null for SkillPart {displayName}.");
            }
            else
            {
                var attack = Instantiate(attackPrefab, targetPosition, targetRotation);
                ConfigureCreatedObject(attack, faction, timeToKeepEffectAliveInSeconds, damageMultiplier);
            }

            return Task.CompletedTask;
        }
    }
}