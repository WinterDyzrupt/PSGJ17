using Arena;
using UnityEngine;

public class BuffDebuff : MonoBehaviour
{
    public string displayName;
    public float buffDuration;
    public GameObject buffVFXPrefab;
    protected CombatantController combatantController;
    [SerializeField]private float _elapsedTime = 0;
    private GameObject _buffVFX;

    void Start()
    {
        TryGetComponent(out combatantController);
        Debug.Assert(combatantController != null, displayName + " couldn't fine a combatantController.");
        ApplyBuffDebuff();
    }

    void Update()
    {
        if (_elapsedTime >= buffDuration) { UninstallBuffDebuff(); }
        else { _elapsedTime += Time.deltaTime; }
    }


    protected virtual void ApplyBuffDebuff()
    {
        if (buffVFXPrefab != null)
        {
            _buffVFX = Instantiate(buffVFXPrefab, transform);
        }
        else { Debug.Log($"{displayName} didn't have a VFX assigned to it."); }
    }

    protected virtual void UninstallBuffDebuff()
    {
        Destroy(_buffVFX);
        Destroy(this);
    }
}
