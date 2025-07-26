using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PersistentData
{
    public class Combatant : ScriptableObject
    {
        public StringReference displayName;
        [TextArea]
        public string description;
        public Sprite sprite;

        public Skill basicAttack;
        public Skill ability1;
        public Skill ability2;
        public Skill utility;

        public FloatReference currentHealth;

        public float MaxHealth => baseMaxHealth + bonusMaxHealth;
        public float baseMaxHealth;
        public float bonusMaxHealth;

        public virtual void SetValues(Combatant combatantToGetValuesFrom)
        {
            Debug.Assert(combatantToGetValuesFrom != null, nameof(combatantToGetValuesFrom) + " expected to be not null.");
            Debug.Assert(this.currentHealth != null, $"{nameof(this.currentHealth)} expected to be not null.");
            Debug.Assert(combatantToGetValuesFrom.currentHealth != null, $"{nameof(combatantToGetValuesFrom.currentHealth)} expected to be not null.");
            
            this.displayName = combatantToGetValuesFrom.displayName;
            this.description = combatantToGetValuesFrom.description;
            this.sprite = combatantToGetValuesFrom.sprite;
            this.basicAttack = combatantToGetValuesFrom.basicAttack;
            this.ability1 = combatantToGetValuesFrom.ability1;
            this.ability2 = combatantToGetValuesFrom.ability2;
            this.utility = combatantToGetValuesFrom.utility;
            
            this.currentHealth.useConstant = combatantToGetValuesFrom.currentHealth.useConstant;
            this.currentHealth.Value = combatantToGetValuesFrom.currentHealth.Value;
            this.baseMaxHealth = combatantToGetValuesFrom.baseMaxHealth;
            this.bonusMaxHealth = combatantToGetValuesFrom.bonusMaxHealth;
        }

        public virtual void ResetValues()
        {
            this.displayName?.ResetValue();
            this.description = string.Empty;
            this.sprite =  null;
            this.basicAttack = null;
            this.ability1 = null;
            this.ability2 = null;
            this.utility = null;

            this.currentHealth?.ResetValue();
            this.baseMaxHealth = 0;
            this.bonusMaxHealth = 0;
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
                $"Name: {displayName}\nDescription: {description}\nMaxHealth: {MaxHealth}\nCurrentHealth: {currentHealth}");
        }
    }
}
