using PersistentData;
using UnityEngine;

namespace Arena
{
    /// <summary>
    /// A tiny wrapper to attach Combatant information to a game object.
    /// TODO: Find a better name when I'm less disgusted with this or find a less disgusting way to do this.
    /// </summary>
    public class CombatantAbomination : MonoBehaviour
    {
        public Combatant combatant;
    }
}