using UnityEngine;

namespace Arena
{
    public class PersistAfterDeath : MonoBehaviour
    {
        public Sprite deadSprite;

        public int deadSpriteLayer = 3;

        private SpriteRenderer _spriteRenderer;
        private PlayerMovement _playerMovement;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            Debug.Assert(deadSprite != null, nameof(deadSprite) + " expected to be not null");
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Debug.Assert(_spriteRenderer != null, nameof(_spriteRenderer) + " expected to be not null");
            _playerMovement = GetComponent<PlayerMovement>();
            Debug.Assert(_playerMovement != null, nameof(_playerMovement) + " expected to be not null");
            _boxCollider2D = GetComponent<BoxCollider2D>();
            Debug.Assert(_boxCollider2D != null, nameof(_boxCollider2D) + " expected to be not null");
        }

        public void OnDeath()
        {
            _spriteRenderer.sprite = deadSprite;
            _spriteRenderer.sortingOrder = deadSpriteLayer;
            _playerMovement.enabled = false;
            _boxCollider2D.enabled = false;
        }
    }
}