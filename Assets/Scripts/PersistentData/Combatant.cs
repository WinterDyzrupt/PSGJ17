using Unity.Mathematics.Geometry;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PersistentData
{
    public class Combatant : ScriptableObject
    {
        private const float DefaultMultiplier = 1f;
        private const int DefaultAvoidAttackIntervalInSeconds = 10;
        private const float MinimumDamageToReceive = 0f;

        public StringReference displayName;
        [TextArea] public string description;
        public Sprite sprite;

        public FloatReference currentHealth;

        public float MaxHealth => baseMaxHealth + bonusMaxHealth;
        public float baseMaxHealth;
        public float bonusMaxHealth;

        public float FlatDamageReduction => baseFlatDamageReduction + bonusFlatDamageReduction;
        public float baseFlatDamageReduction;
        public float bonusFlatDamageReduction;

        public float DamageReceivedMultiplier => baseDamageReceivedMultiplier * bonusDamageReceivedMultiplier;
        public float baseDamageReceivedMultiplier = DefaultMultiplier;
        public float bonusDamageReceivedMultiplier = DefaultMultiplier;

        public float OutgoingDamageMultiplier => baseOutgoingDamageMultiplier * bonusOutgoingDamageMultiplier;
        public float baseOutgoingDamageMultiplier = DefaultMultiplier;
        public float bonusOutgoingDamageMultiplier = DefaultMultiplier;

        public float MovementSpeed => baseMovementSpeed + bonusMovementSpeed;
        public float baseMovementSpeed;
        public float bonusMovementSpeed;

        public float MovementSpeedMultiplier => baseMovementSpeedMultiplier * bonusMovementSpeedMultiplier;
        public float baseMovementSpeedMultiplier = DefaultMultiplier;
        public float bonusMovementSpeedMultiplier = DefaultMultiplier;

        public float CooldownReductionMultiplier => baseCooldownReductionMultiplier * bonusCooldownReductionMultiplier;
        public float baseCooldownReductionMultiplier = DefaultMultiplier;
        public float bonusCooldownReductionMultiplier = DefaultMultiplier;

        public float AvoidAttackCount => baseAvoidAttackCount + bonusAvoidAttackCount;
        public int baseAvoidAttackCount;
        public int bonusAvoidAttackCount;

        public float AvoidAttackIntervalInSeconds =>
            baseAvoidAttackIntervalInSeconds + bonusAvoidAttackIntervalInSeconds;

        public float baseAvoidAttackIntervalInSeconds = DefaultAvoidAttackIntervalInSeconds;
        public float bonusAvoidAttackIntervalInSeconds;

        public virtual void SetValues(Combatant combatantToGetValuesFrom)
        {
            Debug.Assert(combatantToGetValuesFrom != null,
                nameof(combatantToGetValuesFrom) + " expected to be not null.");
            Debug.Assert(this.currentHealth != null, $"{nameof(this.currentHealth)} expected to be not null.");
            Debug.Assert(combatantToGetValuesFrom.currentHealth != null,
                $"{nameof(combatantToGetValuesFrom.currentHealth)} expected to be not null.");

            this.displayName.Value = combatantToGetValuesFrom.displayName.Value;
            this.description = combatantToGetValuesFrom.description;
            this.sprite = combatantToGetValuesFrom.sprite;

            this.currentHealth.useConstant = combatantToGetValuesFrom.currentHealth.useConstant;
            this.currentHealth.Value = combatantToGetValuesFrom.currentHealth.Value;

            this.baseMaxHealth = combatantToGetValuesFrom.baseMaxHealth;
            this.bonusMaxHealth = combatantToGetValuesFrom.bonusMaxHealth;

            this.baseFlatDamageReduction = combatantToGetValuesFrom.baseFlatDamageReduction;
            this.bonusFlatDamageReduction = combatantToGetValuesFrom.bonusFlatDamageReduction;

            this.baseDamageReceivedMultiplier = combatantToGetValuesFrom.baseDamageReceivedMultiplier;
            this.bonusDamageReceivedMultiplier = combatantToGetValuesFrom.bonusDamageReceivedMultiplier;

            this.baseOutgoingDamageMultiplier = combatantToGetValuesFrom.baseOutgoingDamageMultiplier;
            this.bonusOutgoingDamageMultiplier = combatantToGetValuesFrom.bonusOutgoingDamageMultiplier;

            this.baseMovementSpeed = combatantToGetValuesFrom.baseMovementSpeed;
            this.bonusMovementSpeed = combatantToGetValuesFrom.bonusMovementSpeed;

            this.baseMovementSpeedMultiplier = combatantToGetValuesFrom.baseMovementSpeedMultiplier;
            this.bonusMovementSpeedMultiplier = combatantToGetValuesFrom.bonusMovementSpeedMultiplier;

            this.baseCooldownReductionMultiplier = combatantToGetValuesFrom.baseCooldownReductionMultiplier;
            this.bonusCooldownReductionMultiplier = combatantToGetValuesFrom.baseCooldownReductionMultiplier;

            this.baseAvoidAttackCount = combatantToGetValuesFrom.baseAvoidAttackCount;
            this.bonusAvoidAttackCount = combatantToGetValuesFrom.bonusAvoidAttackCount;

            this.baseAvoidAttackIntervalInSeconds = combatantToGetValuesFrom.baseAvoidAttackIntervalInSeconds;
            this.bonusAvoidAttackIntervalInSeconds = combatantToGetValuesFrom.baseAvoidAttackIntervalInSeconds;
        }

        public virtual void ResetValues()
        {
            this.displayName?.ResetValue();
            this.description = string.Empty;
            this.sprite = null;

            this.currentHealth?.ResetValue();

            this.baseMaxHealth = 0;
            this.bonusMaxHealth = 0;

            this.baseFlatDamageReduction = 0f;
            this.bonusFlatDamageReduction = 0f;

            this.baseDamageReceivedMultiplier = DefaultMultiplier;
            this.bonusDamageReceivedMultiplier = DefaultMultiplier;

            this.baseOutgoingDamageMultiplier = DefaultMultiplier;
            this.bonusOutgoingDamageMultiplier = DefaultMultiplier;

            this.baseMovementSpeed = 0f;
            this.bonusMovementSpeed = 0f;

            this.baseMovementSpeedMultiplier = DefaultMultiplier;
            this.bonusMovementSpeedMultiplier = DefaultMultiplier;

            this.baseCooldownReductionMultiplier = DefaultMultiplier;
            this.bonusCooldownReductionMultiplier = DefaultMultiplier;

            this.baseAvoidAttackCount = 0;
            this.bonusAvoidAttackCount = 0;

            this.baseAvoidAttackIntervalInSeconds = 0f;
            this.bonusAvoidAttackIntervalInSeconds = 0f;
        }

        public void ReceiveDamage(float initialDamage)
        {
            var reducedDamage = initialDamage * DamageReceivedMultiplier - FlatDamageReduction;
            var actualDamage = Mathf.Max(reducedDamage, MinimumDamageToReceive);
            Debug.Log($"{displayName} taking initial damage: {initialDamage}; finalDamage: {actualDamage}");

            currentHealth.Value -= actualDamage;
        }

        public override string ToString()
        {
            return string.Format(
                $"Name: {displayName}\nDescription: {description}\nMaxHealth: {MaxHealth}\nCurrentHealth: {currentHealth}");
        }
    }
}
