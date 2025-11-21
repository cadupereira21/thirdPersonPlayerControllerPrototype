using System;
using _MyProject.Scripts.MyGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _MyProject.Scripts.MyGame.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class KinematicPlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float jumpHeight = 5f;
        
        private CharacterController _characterController;
        
        // Actions
        private InputAction _moveAction;
        private InputAction _jumpAction;

        private Camera _camera;

        private void Awake()
        {
            _characterController = this.GetComponent<CharacterController>();
            _camera = Camera.main;
        }

        private void Start()
        {
            _moveAction = InputManager.Instance.PlayerActions.Move;
            _jumpAction = InputManager.Instance.PlayerActions.Jump;
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            ApplyRotation();
            ApplyMovement();
        }

        private void ApplyRotation()
        {
            if (!_moveAction.inProgress) return;
            Vector3 currentLookDirection = _characterController.velocity.normalized;
            currentLookDirection.y = 0;
            currentLookDirection.Normalize();
            
            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void ApplyMovement()
        {
            Vector2 inputValue = _moveAction.ReadValue<Vector2>();
            Vector3 movement = new (inputValue.x, 0, inputValue.y);
            movement = _camera.transform.TransformDirection(movement);

            movement.y = CalculateVerticalForce();
            
            _characterController.Move(movement * (Time.deltaTime * movementSpeed));
        }
        
        private float CalculateVerticalForce()
        {
            float verticalVelocity = -1f;
            if (!_characterController.isGrounded)
            {
                verticalVelocity -= Physics.gravity.y * Time.fixedDeltaTime;
            }
            else
            {
                if (_jumpAction.triggered)
                {
                    Debug.Log($"[KinematicPlayerMovementController] Jump triggered");
                }
            }

            return verticalVelocity;
        }
    }
}