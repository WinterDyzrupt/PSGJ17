using PersistentData;
using UnityEngine;
using TMPro;

namespace Arena
{
    public class Status : MonoBehaviour
    {
        public Combatant combatant;
        public TMP_Text displayNameBox;

        private void Awake()
        {
            if (!combatant)
            {
                Debug.LogWarning($"{nameof(combatant)} was not specified for {nameof(Status)}.");
            }

            if (!displayNameBox)
            {
                Debug.LogWarning($"{nameof(displayNameBox)} was not specified for {nameof(Status)}.");
            }
        }

        private void Start()
        {
            if (combatant && displayNameBox)
            {
                displayNameBox.text = combatant.displayName;
            }
        }

    }
}