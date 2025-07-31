using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyBuffToSelf", menuName = "Scriptable Objects/Skills/Part-ApplyBuffToSelf")]
public class ApplyStatusEffectToSelf : SkillPart
{
    public GameObject statusEffectPrefab;
    public GameObject buffVFXPrefab;
    public float buffDurationInSeconds = 5;

    public override void ExecuteSkill(Transform transform, FactionType faction)
    {
        if (statusEffectPrefab != null && statusEffectPrefab.TryGetComponent(out StatusEffect statusEffectComponent))
        {
            Type statusEffectChildClass = statusEffectComponent.GetClass();

            StatusEffect statusEffect = (StatusEffect)transform.parent.gameObject.AddComponent(statusEffectChildClass);

            statusEffect.buffDurationInSeconds = buffDurationInSeconds;

            if (buffVFXPrefab == null)
            {
                Debug.LogWarning($"{displayName} was not given a VFX prefab.");
            }
            else
            {
                statusEffect.buffVFXPrefab = buffVFXPrefab;
            }
        }
        else
        {
            Debug.LogError($"{displayName} wasn't assigned a script.");
        }
    }
}
