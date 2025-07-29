using UnityEngine;
using UnityEngine.InputSystem;
using PersistentData;

namespace Arena
{
    public class PersistAfterDeath : MonoBehaviour
    {
        public Sprite deadSprite;

        public int deadSpriteLayer = 3;

        private SpriteRenderer _spriteRenderer;
        private PlayerController _playerController;
        private PlayerInput _playerInput;
        private BoxCollider2D _boxCollider2D;
        private SpriteRenderer _warriorDirectionIndicator;

        private void Awake()
        {
            Debug.Assert(deadSprite != null, nameof(deadSprite) + " expected to be not null");
            _playerController = GetComponent<PlayerController>();
            Debug.Assert(_playerController != null, nameof(_playerController) + " expected to be not null");
            _spriteRenderer = _playerController.combatantSprite;
            Debug.Assert(_spriteRenderer != null, nameof(_spriteRenderer) + " expected to be not null");
            _boxCollider2D = GetComponent<BoxCollider2D>();
            Debug.Assert(_boxCollider2D != null, nameof(_boxCollider2D) + " expected to be not null");
            _playerInput = GetComponent<PlayerInput>();
            Debug.Assert(_playerInput != null, nameof(_playerInput) + " expected to be not null");
            _warriorDirectionIndicator = _playerController.warriorDirectionIndicator;
            Debug.Assert(_warriorDirectionIndicator != null, nameof(_playerController) + " didn't have a direction arrow.");
        }

        public void OnDeath()
        {
            RemoveStatusEffect();
            gameObject.tag = Tags.Untagged;
            _spriteRenderer.sprite = deadSprite;
            _spriteRenderer.sortingOrder = deadSpriteLayer;
            _playerController.enabled = false;
            _boxCollider2D.enabled = false;
            _playerInput.enabled = false;
            _warriorDirectionIndicator.enabled = false;
        }

        private void RemoveStatusEffect()
        {
            StatusEffect[] statusEffects = GetComponents<StatusEffect>();

            foreach (StatusEffect statusEffect in statusEffects) { statusEffect.RemoveStatusEffect(); }
        }
    }
}