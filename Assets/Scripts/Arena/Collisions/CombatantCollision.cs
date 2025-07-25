using PersistentData;
using UnityEngine;
using UnityEngine.Events;

namespace Arena.Collisions
{
    public class CombatantCollision : MonoBehaviour
    {
        /// <summary>
        /// Self, the combatant that this is attached to.
        /// </summary>
        [Tooltip("Self, the combatant that this is attached to.")]
        public Combatant currentCombatant;

        public UnityEvent damageEvent;
        public UnityEvent deathEvent;

        private FactionType _faction;

        public bool resetCurrentHealth = true;

        private void Start()
        {
            if (TryGetComponent(out Faction factionComponent))
            {
                _faction = factionComponent.faction;
            }
            else
            {
                Debug.LogError($"{gameObject.name} doesn't have a faction and can take damage!");
            }

            Debug.Assert(currentCombatant != null, $"{nameof(currentCombatant)} was null");
            if (resetCurrentHealth)
            {
                currentCombatant.currentHealth.Value = currentCombatant.maxHealth.Value;
            }
        }

        /// <summary>
        /// Finds out if the collision was with something that deals damage on collision, and raises a damage/death event
        /// </summary>
        /// <param name="otherObject"></param>
        private void OnCollisionEnter2D(Collision2D otherObject)
        {
            if (otherObject.gameObject.TryGetComponent<DealsDamageOnCollision>(out var dealsDamageOnCollision))
            {
                FactionType otherFaction = otherObject.gameObject.GetComponent<Faction>().faction;

                Debug.Log($"Damage Detected: from {otherObject.gameObject.name} with {otherFaction} to {gameObject.name} with {_faction}!");

                if (_faction != otherFaction)
                {
                    currentCombatant.ReceiveDamage(dealsDamageOnCollision.damage.Value);
                    damageEvent?.Invoke();

                    if (currentCombatant.currentHealth <= 0f)
                    {
                        deathEvent?.Invoke();
                    }
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D otherObject)
        {
            if (otherObject.gameObject.TryGetComponent<DealsDamageOnCollision>(out var dealsDamageOnCollision))
            {
                FactionType otherFaction = otherObject.gameObject.GetComponent<Faction>().faction;

                Debug.Log($"Damage Detected: from {otherObject.name} with {otherFaction} to {gameObject.name} with {_faction}!");

                if (_faction != otherFaction)
                {
                    currentCombatant.ReceiveDamage(dealsDamageOnCollision.damage.Value);
                    damageEvent?.Invoke();

                    if (currentCombatant.currentHealth <= 0f)
                    {
                        deathEvent?.Invoke();
                    }
                }
            }
        }
    }
}
