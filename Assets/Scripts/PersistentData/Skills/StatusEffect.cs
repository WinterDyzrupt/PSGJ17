using Arena.Collisions;
using PersistentData;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public string displayName;
    public float buffDuration;
    public GameObject buffVFXPrefab;
    protected Combatant combatant;
    [SerializeField]private float _elapsedTime = 0;
    private GameObject _buffVFX;

    void Start()
    {
        if (TryGetComponent(out CombatantCollision combatantCollision))
        {
            combatant = combatantCollision.currentCombatant;
        }

        Debug.Assert(combatant != null, displayName + " couldn't fine a combatantController.");

        ApplyStatusEffect();
    }

    void Update()
    {
        if (_elapsedTime >= buffDuration) { RemoveStatusEffect(); }
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
