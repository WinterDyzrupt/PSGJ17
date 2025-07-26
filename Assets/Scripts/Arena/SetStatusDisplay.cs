using PersistentData;
using PersistentData.Bosses;
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

        private int _numberOfPhases;

        private void Awake()
        {
            Debug.Assert(combatant != null, $"Expected {nameof(combatant)} to be set.");
            Debug.Assert(displayNameBox != null, $"Expected {nameof(displayNameBox)} to be set.");
            Debug.Assert(healthBarImage != null, $"Expected {nameof(healthBarImage)} to be set.");
        }

        private void Start()
        {
            if (combatant is Boss boss)
            {
                _numberOfPhases = boss.numberOfPhases;
            }

            displayNameBox.text = combatant.displayName;
        }

        private void Update()
        {
            // For when current warrior changes
            displayNameBox.text = combatant.displayName;
            healthBarImage.fillAmount = combatant.currentHealth.Value / combatant.MaxHealth;

            if (_numberOfPhases > 0)
            {
                healthBarImage.material.mainTextureScale = new Vector2(_numberOfPhases, 1);
            }
        }
    }
}