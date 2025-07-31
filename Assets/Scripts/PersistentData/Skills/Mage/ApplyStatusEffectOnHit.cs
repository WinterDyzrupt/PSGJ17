using Arena.Collisions;
using UnityEngine;

public class ApplyStatusEffectOnHit : MonoBehaviour
{
    public Faction faction;
    public GameObject statusEffectPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // get combatant and component of the enemy
        GameObject other = collision.gameObject;
        Faction otherFaction;
        if (other.TryGetComponent(out CombatantCollision otherCombatantCollision) && otherCombatantCollision.currentCombatant != null)
        {
            if (other.TryGetComponent(out otherFaction)) { }
            else
            {
                Debug.Log("Couldn't get a Faction.");
                return;
            }
        }
        else
        {
            Debug.Log("Couldn't get a combattant.");
            return;
        }

        if (otherFaction.faction != FactionType.Other && otherFaction.faction != faction.faction)
        {
            Debug.Log($"Name of Target {other.name}.");
            ApplyStatusEffect(other);
        }
    }

    private void ApplyStatusEffect(GameObject target)
    {
        target.AddComponent(statusEffectPrefab.GetComponent<StatusEffect>().GetClass());
    }
}
