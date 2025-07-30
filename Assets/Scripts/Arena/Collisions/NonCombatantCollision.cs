using UnityEngine;

namespace Arena.Collisions
{
    public class NonCombatantCollision : MonoBehaviour
    {
        private FactionType _faction;

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

                Debug.Log(
                    $"Damage Detected: from {otherObject.gameObject.name} with {otherFaction} to {gameObject.name} with {_faction}!");

                if (_faction != otherFaction)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D otherObject)
        {
            if (otherObject.gameObject.TryGetComponent<DealsDamageOnCollision>(out var dealsDamageOnCollision))
            {
                FactionType otherFaction = otherObject.gameObject.GetComponent<Faction>().faction;

                Debug.Log(
                    $"Collision with possible damage: from {otherObject.name} with faction: {otherFaction} to {gameObject.name} of faction: {_faction}!");

                if (_faction != otherFaction)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}