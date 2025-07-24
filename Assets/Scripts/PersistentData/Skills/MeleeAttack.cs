using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Skills/Part-MeleeAttack")]
public class MeleeAttack : SkillPart
{
    public float offset = 35;
    public GameObject shapePrefab;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Transform transform)
    {
        if (shapePrefab != null)
        {
            GameObject shape = Instantiate(shapePrefab, transform);
            shape.transform.position += Vector3.right * offset;
        }
        else
        {
            Debug.LogError($"{displayName} doesn't have a prefab shape.");
        }
    }
}
