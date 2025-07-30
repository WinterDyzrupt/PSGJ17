using PersistentData.Bosses;
using UnityEngine;
using UnityEngine.Events;

namespace Arena
{
    public class BossPhaseMonitor : MonoBehaviour
    {
        public Boss currentBoss;
        public int currentPhaseNumber;
        public UnityEvent bossPhaseChangeEvent;

        private const int BasePhaseNumber = 1;
        private float _healthPerPhase;

        private void Start()
        {
            Debug.Assert(currentBoss != null, $"{nameof(currentBoss)} expected to be not null.");
            Debug.Assert(currentBoss.currentHealth != null, $"{nameof(currentBoss.currentHealth)} expected to be not null.");
            _healthPerPhase = currentBoss.MaxHealth / currentBoss.numberOfPhases;
        }

        public void CheckCurrentPhase()
        {
            // Example values: 4000 max hp / 5 phases = 800 hp per phase
            // max health - current health = missing health
            // missing health / hp per phase =  current phase
            // current health   missingHealth   current phase
            // 3800             200             .25         (0 = initial phase)
            // 3150             850             1.08
            // 2400             1600            2
            // 1601             2399            2.99875
            // 1599             2401            3.00125
            // 760              3240            4.05
            var missingHealth = currentBoss.MaxHealth - currentBoss.currentHealth.Value; 
            var newPhaseNumber = Mathf.FloorToInt(missingHealth / _healthPerPhase) + BasePhaseNumber;

            if (newPhaseNumber > currentPhaseNumber)
            { 
                currentPhaseNumber = newPhaseNumber;
                bossPhaseChangeEvent.Invoke();
            }
        }
    }
}
