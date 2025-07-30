using UnityEngine;

[CreateAssetMenu(fileName = "CreateSummon", menuName = "Scriptable Objects/Skills/Part-CreateSummon")]
public class CreateSummon : SkillPart
{
    // Skill Class for a Melee Attack
    public GameObject summonGameObject;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier, Vector3 targetPosition, Quaternion targetRotation)
    {
        if (summonGameObject == null)
        {
            Debug.LogError($"{displayName} skill is missing its summoned GameObject.");
        }
        else
        {
            var createdSummon = Instantiate(summonGameObject, targetPosition, targetRotation);

            // Grab the class that operate the summon
            // Summon summon = createdSummon.GetComponent<Summon>();

            // give the summon information about what it's doing and who made it
            // where it is placed might be based on character and direction or mouse cursor
            // therefore, it should handle its transform
            // summon.Initialize(combatant, this);

            // make sure it's not attached to the combatant
            createdSummon.transform.SetParent(null);
        }
    }
}
