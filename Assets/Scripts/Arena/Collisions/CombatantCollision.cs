using Arena;
using Events;
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

        public bool resetCurrentHealth = true;
        
        private void Start()
        {
            Debug.Assert(currentCombatant != null,  $"{nameof(currentCombatant)} was null");
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
