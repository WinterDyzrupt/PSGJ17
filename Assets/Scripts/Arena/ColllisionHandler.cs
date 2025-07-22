using System;
using PersistentData;
using UnityEngine;

namespace Arena
{
    /// <summary>
    /// Handles collisions; this should be attached to anything that does damage to a combatant, not non-damaging objects.
    /// </summary>
    public class CollisionHandler : MonoBehaviour
    {
        public IntReference collisionDamage;

        /// <summary>
        /// Default collision damage; a large amount, for testing, for clear collisions and quick conclusions.
        /// </summary>
        private const int DefaultCollisionDamage = 25;

        private void Awake()
        {
            if (collisionDamage == null)
            {
                collisionDamage = new IntReference()
                {
                    useConstant = true,
                    constantValue = DefaultCollisionDamage
                };
            }
        }

        /// <summary>
        /// Finds out if the collision was with a combatant, and applies damage.
        /// </summary>
        /// <param name="otherObject"></param>
        private void OnCollisionEnter2D(Collision2D otherObject)
        {
            if (otherObject.gameObject.TryGetComponent<CombatantAbomination>(out var otherCombatantAbomination))
            {
                var otherCombatant = otherCombatantAbomination.combatant;
                otherCombatant.ReceiveDamage(collisionDamage.Value);
            }
        }
    }
}
