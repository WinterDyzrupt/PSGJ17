using PersistentData;
using UnityEngine;
using UnityEngine.UI;

namespace Arena
{
    public class HealthBarFiller : MonoBehaviour
    {
        public Image image;
        public Combatant combatant;

        private void Update()
        {
            image.fillAmount = (float)combatant.currentHealth.Value / combatant.maxHealth.Value;
        }
    }
}