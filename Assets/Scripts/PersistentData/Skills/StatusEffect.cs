using Arena.Collisions;
using PersistentData;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public string displayName;
    public float buffDurationInSeconds;
    public GameObject buffVFXPrefab;
    protected Combatant combatant;
    private float _elapsedTime = 0;
    private GameObject _buffVFX;

    void Start()
    {
        if (TryGetComponent(out CombatantCollision combatantCollision))
        {
            combatant = combatantCollision.currentCombatant;
            ApplyStatusEffect();
        }
        else
        {
            Debug.Assert(combatant != null, displayName + " couldn't fine a combatant.");
        }
    }

    void Update()
    {
        if (_elapsedTime >= buffDurationInSeconds) { RemoveStatusEffect(); }
        else { _elapsedTime += Time.deltaTime; }
    }


    protected virtual void ApplyStatusEffect()
    {
        if (buffVFXPrefab != null)
        {
            _buffVFX = Instantiate(buffVFXPrefab, transform);
        }
        else { Debug.Log($"{displayName} didn't have a VFX assigned to it."); }
    }

    public virtual void RemoveStatusEffect()
    {
        Destroy(_buffVFX);
        Destroy(this);
    }
}