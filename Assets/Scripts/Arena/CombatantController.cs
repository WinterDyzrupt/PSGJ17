using PersistentData;
using UnityEngine;

namespace Arena
{
    public class CombatantController : MonoBehaviour
    {
        public Rigidbody2D combatantRigidbody;
        public Transform orientationTransform;
        public SpriteRenderer combatantSprite;
        public Combatant currentCombatant;

        protected FactionType _faction;
        protected Vector2 _moveDirection = new();
        protected Vector2 _aimDirection = new();

        void Awake()
        {
            Debug.Assert(currentCombatant != null, $"{gameObject.name} hasn't been assigned to the CombatController");
        }

        void Start()
        {
            if (TryGetComponent(out Faction factionComponent)) _faction = factionComponent.faction;
            else Debug.LogError($"{gameObject.name} doesn't have a faction.");
            Debug.Assert(combatantRigidbody != null, $"{gameObject.name} doesn't have a Rigidbody assigned.");
            Debug.Assert(orientationTransform != null, $"{gameObject.name} doesn't have a Orientation Transform assigned.");
            Debug.Assert(combatantSprite != null, $"{gameObject.name} doesn't have a sprite assigned.");
        }

        void LateUpdate()
        {
            RotateOrientation();
        }

        void FixedUpdate()
        {
            combatantRigidbody.linearVelocity = (currentCombatant.MovementSpeed * currentCombatant.MovementSpeedMultiplier) * _moveDirection;
        }

        public void UpdateMoveDirection(Vector2 newMoveDirection)
        {
            _moveDirection = newMoveDirection;
        }

        public void UpdateAimDirection(Vector2 newAimDirection)
        {
            _aimDirection = newAimDirection;
        }

        private void RotateOrientation()
        {
            if (_aimDirection.magnitude > .1) // controller deadzone so it doesn't default to right
            {
                float angleOfOrientation = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;

                orientationTransform.rotation = Quaternion.Euler(0.0f, 0.0f, angleOfOrientation);

                if (angleOfOrientation > 90 || angleOfOrientation < -90) combatantSprite.flipX = true;
                else combatantSprite.flipX = false;
            }
        }

        public void ExecuteSkill(Skill skill)
        {
            if (skill != null)
            {
                skill.ExecuteSkill(
                    orientationTransform,
                    _faction,
                    currentCombatant.CooldownReductionMultiplier,
                    currentCombatant.OutgoingDamageMultiplier);
            }
            else
            {
                Debug.LogWarning($"Attempted to activate skill on {gameObject.name} but was given null. Was it supposed to be missing?");
            }
        }
    }
}