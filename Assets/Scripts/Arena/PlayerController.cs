using PersistentData.Warriors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Arena
{
    [RequireComponent(typeof(ArenaControls))]
    public class PlayerController : CombatantController
    {
        public Warrior currentWarrior;
        private ArenaControls _arenaControls;
        private InputAction _inputMove;
        private InputAction _inputAim;
        private InputAction _inputBasicAttack;
        private InputAction _inputAbility1;
        private InputAction _inputAbility2;
        private InputAction _inputUtility;
        private bool _isUsingGamepad;

        private void Awake()
        {
            Debug.Assert(currentWarrior != null, $"Current Warrior hasn't been assigned to the PlayerController");

            _arenaControls = new();
        }

        private void Update()
        {
            UpdateMoveDirection(_inputMove.ReadValue<Vector2>());
            if (!_isUsingGamepad)
            {
                UpdateAimDirection((Vector2)(Camera.main.ScreenToWorldPoint(_inputAim.ReadValue<Vector2>()) - transform.position));
            }
            else
            {
                UpdateAimDirection(_inputAim.ReadValue<Vector2>());
            }

            // Checks to see if any of the skill buttons are held down
            // Let the cooldown feature dictate when skills are activated
            if (_inputBasicAttack.IsPressed()) ExecuteSkill(currentWarrior.basicAttack);
            if (_inputAbility1.IsPressed()) ExecuteSkill(currentWarrior.ability1);
            if (_inputAbility2.IsPressed()) ExecuteSkill(currentWarrior.ability2);
            if (_inputUtility.IsPressed()) ExecuteSkill(currentWarrior.utility);
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
        }
    }
}
