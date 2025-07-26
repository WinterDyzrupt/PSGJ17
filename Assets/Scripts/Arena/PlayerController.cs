using PersistentData.Warriors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Arena
{
    /// <summary>
    /// Something to activate skills and pass information down to the skills
    /// </summary>
    [RequireComponent(typeof(ArenaControls))]
    public class PlayerController : MonoBehaviour
    {
        public Warrior currentWarrior;
        public Rigidbody2D warriorRigidBody;
        public Transform orientationTransform;
        public SpriteRenderer warriorSprite;

        private FactionType _faction;
        private ArenaControls _arenaControls;
        private InputAction _inputMove;
        private InputAction _inputAim;
        private InputAction _inputBasicAttack;
        private InputAction _inputAbility1;
        private InputAction _inputAbility2;
        private InputAction _inputUtility;
        [SerializeField] private bool _isUsingGamepad;
        private Vector2 _moveDirection = new();
        private Vector2 _aimDirection = new();
        private float _moveSpeed = 200;

        private void Awake()
        {
            if (currentWarrior == null)
            {
                Debug.LogError($"Current Warrior hasn't been assigned to the PlayerController");
            }

            _arenaControls = new();
        }

        private void Start()
        {
            if (TryGetComponent(out Faction factionComponent))
            {
                _faction = factionComponent.faction;
            }
            else
            {
                Debug.LogError($"{gameObject.name} doesn't have a faction.");
            }
        }

        private void Update()
        {
            _moveDirection = _inputMove.ReadValue<Vector2>();
            _aimDirection = _inputAim.ReadValue<Vector2>();

            // Checks to see if any of the skill buttons are held down
            // Let the cooldown feature dictate when skills are activated
            if (_inputBasicAttack.IsPressed()) ExecuteSkill(currentWarrior.basicAttack);
            if (_inputAbility1.IsPressed()) ExecuteSkill(currentWarrior.ability1);
            if (_inputAbility2.IsPressed()) ExecuteSkill(currentWarrior.ability2);
            if (_inputUtility.IsPressed()) ExecuteSkill(currentWarrior.utility);

            RotatePlayer();
        }

        void FixedUpdate()
        {
            warriorRigidBody.linearVelocity = _moveDirection * _moveSpeed;
        }

        private void RotatePlayer()
        {
            if (!_isUsingGamepad)
            {
                _aimDirection = (Vector2)(Camera.main.ScreenToWorldPoint(_aimDirection) - transform.position);
            }

            if (_aimDirection.magnitude != 0)
            {
                var angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
                orientationTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            // Should we flip the sprite based on the rotation of the orientation
            float angleOfOrientation = orientationTransform.eulerAngles.z;
            if (angleOfOrientation > 90 && angleOfOrientation < 270) warriorSprite.flipX = true;
            else warriorSprite.flipX = false;
        }

        void OnEnable()
        {
            _inputMove = _arenaControls.Warrior.Move;
            _inputMove.Enable();

            _inputAim = _arenaControls.Warrior.Aim;
            _inputAim.Enable();

            _inputBasicAttack = _arenaControls.Warrior.BasicAttack;
            _inputBasicAttack.Enable();

            _inputAbility1 = _arenaControls.Warrior.Ability1;
            _inputAbility1.Enable();

            _inputAbility2 = _arenaControls.Warrior.Ability2;
            _inputAbility2.Enable();

            _inputUtility = _arenaControls.Warrior.Utility;
            _inputUtility.Enable();
        }

        void OnDisable()
        {
            _arenaControls.Disable();

            _inputMove.Disable();

            _inputAim.Disable();

            _inputBasicAttack.Disable();

            _inputAbility1.Disable();

            _inputAbility2.Disable();

            _inputUtility.Disable();
        }

        public void OnDeviceChange(PlayerInput playerInput)
        {
            _isUsingGamepad = playerInput.currentControlScheme.Equals("Gamepad");
            if (_isUsingGamepad) Debug.Log("we're using the gamepad!");
            else Debug.Log("We're using the mouse!");
        }


        public void ExecuteSkill(Skill skill)
        {
            if (skill != null)
            {
                skill.ExecuteSkill(orientationTransform, _faction);
            }
            else
            {
                Debug.LogWarning($"Attempted to activate skill on {currentWarrior.displayName} but it was empty. Was it supposed to be missing?");
            }
        }
    }
}
