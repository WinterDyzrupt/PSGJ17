using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyBuffToSelf", menuName = "Scriptable Objects/Skills/Part-ApplyBuffToSelf")]
public class ApplyBuffToSelf : SkillPart
{
    public MonoScript buffDebuffChild;
    public GameObject buffVFXPrefab;
    public float buffDuration = 5;

    public override void ExecuteSkill(Transform transform, FactionType faction, float damageMultiplier = 1)
    {
        if (buffDebuffChild == null)
        {
            Debug.LogError($"{displayName} wasn't assigned a script.");
        }
        else
        {
            Type buffClass = buffDebuffChild.GetClass();

            if (buffClass == null || !typeof(BuffDebuff).IsAssignableFrom(buffClass))
            {
                Debug.LogError($"The script given to {displayName} wasn't of class BuffDebuff.");
            }
            else
            {
                BuffDebuff buffComponent = (BuffDebuff)transform.parent.gameObject.AddComponent(buffClass);
                if (buffVFXPrefab == null)
                {
                    Debug.LogWarning($"{displayName} was not given a VFX prefab.");
                }
                else
                {
                    buffComponent.buffDuration = buffDuration;
                    buffComponent.buffVFXPrefab = buffVFXPrefab;
                }
            }
        }
    }
}
