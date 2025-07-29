using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyBuffToSelf", menuName = "Scriptable Objects/Skills/Part-ApplyBuffToSelf")]
public class ApplyStatusEffectToSelf : SkillPart
{
    public MonoScript statusComponent;
    public GameObject buffVFXPrefab;
    public float buffDurationInSeconds = 5;

    public override void ExecuteSkill(Transform transform, FactionType faction)
    {
        if (statusComponent == null)
        {
            Debug.LogError($"{displayName} wasn't assigned a script.");
        }
        else
        {
            Type statusEffectClass = statusComponent.GetClass();

            if (typeof(StatusEffect).IsAssignableFrom(statusEffectClass))
            {
                StatusEffect statusEffectComponent = (StatusEffect)transform.parent.gameObject.AddComponent(statusEffectClass);

                statusEffectComponent.buffDurationInSeconds = buffDurationInSeconds;

                if (buffVFXPrefab == null)
                {
                    Debug.LogWarning($"{displayName} was not given a VFX prefab.");
                }
                else
                {
                    statusEffectComponent.buffVFXPrefab = buffVFXPrefab;
                }
            }
            else
            {
                Debug.LogError($"The script given to {displayName} wasn't of class StatusEffect.");
            }
        }
    }
}
