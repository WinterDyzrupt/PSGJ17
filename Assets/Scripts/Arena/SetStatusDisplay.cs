using PersistentData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Arena
{
    public class SetStatus : MonoBehaviour
    {
        public Combatant combatant;
        public TMP_Text displayNameBox;
        public Image healthBarImage;

        private void Awake()
        {
            Debug.Assert(combatant != null, $"Expected {nameof(combatant)} to be set.");
            Debug.Assert(displayNameBox != null, $"Expected {nameof(displayNameBox)} to be set.");
            Debug.Assert(healthBarImage != null, $"Expected {nameof(healthBarImage)} to be set.");
        }

        private void Start()
        {
            displayNameBox.text = combatant.displayName;
        }

        private void Update()
        {
            // For when current warrior changes
            displayNameBox.text = combatant.displayName;
            healthBarImage.fillAmount = combatant.currentHealth.Value / combatant.maxHealth.Value;
        }
    }
}