using Arena;
using UnityEngine;

public class BuffDebuff : MonoBehaviour
{
    public string displayName;
    public float duration;
    private float _elapsedTime = 0;
    protected CombatantController combatantController;
    void Start()
    {
        TryGetComponent(out combatantController);
        Debug.Assert(combatantController != null, displayName + " couldn't fine a combatantController.");
        ApplyBuffDebuff();
    }

    void Update()
    {
        if (_elapsedTime >= duration) { UninstallBuffDebuff(); }
        else { _elapsedTime = Time.deltaTime; }
    }


    protected virtual void ApplyBuffDebuff() { }

    protected virtual void UninstallBuffDebuff()
    {
        Destroy(this);
    }
}
