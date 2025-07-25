using Arena.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Skills/Part-MeleeAttack")]
public class MeleeAttack : SkillPart
{
    public float offsetFromTheSpawner = 35;
    public GameObject shapePrefab;

    // Override Execute skill so that it performs the attack
    public override void ExecuteSkill(Transform transform, FactionType faction)
    {
        if (shapePrefab != null)
        {
            GameObject shape = Instantiate(shapePrefab, transform);
            shape.transform.localPosition += Vector3.right * offsetFromTheSpawner / transform.localScale.x;
            shape.AddComponent<Faction>().faction = faction;
            shape.AddComponent<DestroyAfterFirstFrame>();
            shape.transform.SetParent(null);
            shape.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogError($"{displayName} doesn't have a prefab shape.");
        }
    }
}
