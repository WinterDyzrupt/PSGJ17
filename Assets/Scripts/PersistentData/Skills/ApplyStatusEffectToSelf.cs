using System;
using PersistentData;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyBuffToSelf", menuName = "Scriptable Objects/Skills/Part-ApplyBuffToSelf")]
public class ApplyStatusEffectToSelf : SkillPart
{
    public MonoScript statusComponent;
    public GameObject buffVFXPrefab;
    public float buffDurationInSeconds = 5;

    public override void ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier = DefaultCombatData.DefaultMultiplier)
    {
        if (statusComponent == null)
        {
            Debug.LogError($"{displayName} wasn't assigned a script.");
        }
        else
        {
            Type buffClass = statusComponent.GetClass();

            if (!typeof(StatusEffect).IsAssignableFrom(buffClass))
            {
                Debug.LogError($"The script given to {displayName} wasn't of class BuffDebuff.");
            }
            else
            {
                StatusEffect buffComponent = (StatusEffect)transform.parent.gameObject.AddComponent(buffClass);

                buffComponent.buffDuration = buffDurationInSeconds;

                if (buffVFXPrefab == null)
                {
                    Debug.LogWarning($"{displayName} was not given a VFX prefab.");
                }
                else
                {
                    buffComponent.buffVFXPrefab = buffVFXPrefab;
                }
            }
        }
    }
}
