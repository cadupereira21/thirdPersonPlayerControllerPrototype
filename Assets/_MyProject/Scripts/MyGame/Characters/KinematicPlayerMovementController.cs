using System;
using _MyProject.Scripts.MyGame.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _MyProject.Scripts.MyGame.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class KinematicPlayerMovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private float rotationSpeed = 7f;
        
        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight = 5f;
        public bool isGrounded;
        
        private CharacterController _characterController;
        
        // Actions
        private InputAction _moveAction;
        private InputAction _runAction;
        private InputAction _jumpAction;

        private Camera _camera;
        
        private float _verticalVelocity = 0f;

        public float HorizontalVelocity => new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z).magnitude;

        private void Awake()
        {
            _characterController = this.GetComponent<CharacterController>();
            _camera = Camera.main;
        }

        private void Start()
        {
            _moveAction = InputManager.Instance.PlayerActions.Move;
            _runAction = InputManager.Instance.PlayerActions.Run;
            _jumpAction = InputManager.Instance.PlayerActions.Jump;
        }

        private void Update()
        {
            isGrounded = _characterController.isGrounded;
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
            float movementSpeed = _runAction.inProgress ? runSpeed : walkSpeed;
            movement = _camera.transform.TransformDirection(movement) * movementSpeed;

            movement.y = CalculateVerticalForce();
            
            _characterController.Move(movement * (Time.deltaTime));
        }
        
        private float CalculateVerticalForce()
        {
            if (!isGrounded)
            {
                _verticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;
            }
            else
            {
                _verticalVelocity = -.1f;
                
                if (_jumpAction.triggered)
                {
                    Debug.Log($"[KinematicPlayerMovementController] Jump triggered");
                    _verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * -Physics.gravity.y);
                }
            }

            return _verticalVelocity;
        }
    }
}