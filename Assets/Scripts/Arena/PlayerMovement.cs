using PersistentData;
using UnityEngine;

namespace Arena
{
    /// <summary>
    /// Enables movement for the player, mostly auto-complete code from the IDE and some tutorials.
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        public FloatReference movementSpeed;
        public Rigidbody2D rigidBody;

        private const float DefaultMovementSpeed = 200f;
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private Vector2 _directionOfMovement;

        private void Awake()
        {
            if (movementSpeed == null)
            {
                Debug.LogWarning("Movement speed not set; setting temporary movement speed.");
                movementSpeed = new FloatReference()
                {
                    useConstant = true,
                    constantValue = DefaultMovementSpeed
                };
            }
        }

        /// <summary>
        /// Allows the player to rotate at any time.
        /// </summary>
        private void Update()
        {
            var horizontalInput = Input.GetAxis(HorizontalAxis);
            var verticalInput = Input.GetAxis(VerticalAxis);

            _directionOfMovement = new Vector2(horizontalInput, verticalInput);
            RotatePlayer(horizontalInput, verticalInput);
        }

        /// <summary>
        /// Move the player every frame
        /// TODO: See if movement feels nicer/more consistent in Update or FixedUpdate. 
        /// </summary>
        private void FixedUpdate()
        {
            rigidBody.linearVelocity = _directionOfMovement * movementSpeed.Value;
        }

        private void RotatePlayer(float horizontalInput, float verticalInput)
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                var angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}
