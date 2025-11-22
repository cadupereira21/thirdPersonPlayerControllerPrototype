using System;
using UnityEngine;

namespace _MyProject.Scripts.MyGame.Characters
{
    public abstract class PlayerAnimations
    {
        public static readonly int HorizontalVelocityParam = Animator.StringToHash("HorizontalVelocity");
        public static readonly int VerticalVelocityParam = Animator.StringToHash("VerticalVelocity");
        public static readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");
    }
    
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private KinematicPlayerBasicMovementController playerBasicMovementController;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }

        public void Update()
        {
            if (playerBasicMovementController.isGrounded)
            {
                _animator.SetFloat(PlayerAnimations.HorizontalVelocityParam, playerBasicMovementController.HorizontalVelocity);
            }
            
            _animator.SetFloat(PlayerAnimations.VerticalVelocityParam, playerBasicMovementController.VerticalVelocity);
            _animator.SetBool(PlayerAnimations.IsGroundedParam, playerBasicMovementController.isGrounded);
        }
    }
}