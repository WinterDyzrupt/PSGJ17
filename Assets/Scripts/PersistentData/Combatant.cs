using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PersistentData
{
    public class Combatant : ScriptableObject
    {
        public StringReference displayName;
        [TextArea]
        public string description;
        public FloatReference maxHealth;
        public FloatReference currentHealth;
        public Sprite sprite;

        public void SetValues(Combatant combatantToGetValuesFrom)
        {
            Debug.Assert(combatantToGetValuesFrom != null, nameof(combatantToGetValuesFrom) + " expected to be not null.");
            Debug.Assert(this.maxHealth != null, $"{nameof(this.maxHealth)} expected to be not null.");
            Debug.Assert(this.currentHealth != null, $"{nameof(this.currentHealth)} expected to be not null.");
            Debug.Assert(combatantToGetValuesFrom.maxHealth != null, $"{nameof(combatantToGetValuesFrom.maxHealth)} expected to be not null.");
            Debug.Assert(combatantToGetValuesFrom.currentHealth != null, $"{nameof(combatantToGetValuesFrom.currentHealth)} expected to be not null.");

            this.displayName = combatantToGetValuesFrom.displayName;
            this.description = combatantToGetValuesFrom.description;
            this.maxHealth.useConstant = combatantToGetValuesFrom.maxHealth.useConstant;
            this.maxHealth.Value = combatantToGetValuesFrom.maxHealth.Value;
            this.currentHealth.useConstant = combatantToGetValuesFrom.currentHealth.useConstant;
            this.currentHealth.Value = combatantToGetValuesFrom.currentHealth.Value;
            this.sprite = combatantToGetValuesFrom.sprite;
        }

        public void Reset()
        {
            this.displayName?.ResetValue();
            this.description = string.Empty;
            this.maxHealth?.ResetValue();
            this.currentHealth?.ResetValue();
        }

        public void ReceiveDamage(float initialDamage)
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
