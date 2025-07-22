using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PersistentData
{
    public class Combatant : ScriptableObject
    {
        public string displayName;
        [TextArea]
        public string description;
        public IntReference maxHealth;
        public IntReference currentHealth;

        public void SetValues(Combatant combatantToGetValuesFrom)
        {
            this.description = combatantToGetValuesFrom.description;
            this.displayName = combatantToGetValuesFrom.displayName;
            if (this.maxHealth != null && combatantToGetValuesFrom.currentHealth != null)
            {
                this.maxHealth.useConstant = combatantToGetValuesFrom.maxHealth.useConstant;
                this.maxHealth.Value = combatantToGetValuesFrom.maxHealth.Value;
            }
            else
            {
                Debug.LogError($"{nameof(this.maxHealth)} was not populated.");
            }

            if (this.currentHealth != null && combatantToGetValuesFrom.currentHealth != null)
            {
                this.currentHealth.useConstant = combatantToGetValuesFrom.currentHealth.useConstant;
                this.currentHealth.Value = combatantToGetValuesFrom.currentHealth.Value;
            }
            else
            {
                Debug.LogError($"{nameof(this.currentHealth)} was not populated.");
            }
        }

        public void Reset()
        {
            this.description = string.Empty;
            this.name = string.Empty;
            this.maxHealth?.ResetValue();
            this.currentHealth?.ResetValue();
        }

        public void ReceiveDamage(int initialDamage)
        {
            Debug.Log($"{displayName} taking damage: {initialDamage}");

            // TODO: Once defense/etc. is added, fill out damage calculation.
            currentHealth.Value -= initialDamage;
        }

        public override string ToString()
        {
            return string.Format(
                $"Name: {displayName}\nDescription: {description}\nMaxHealth: {maxHealth}\nCurrentHealth: {currentHealth}");
        }
    }
}