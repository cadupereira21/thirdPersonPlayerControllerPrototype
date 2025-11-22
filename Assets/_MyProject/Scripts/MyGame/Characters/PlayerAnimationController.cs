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
        [SerializeField] private KinematicPlayerMovementController playerMovementController;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }

        public void Update()
        {
            if (playerMovementController.isGrounded)
            {
                _animator.SetFloat(PlayerAnimations.HorizontalVelocityParam, playerMovementController.HorizontalVelocity);
            }
            
            _animator.SetFloat(PlayerAnimations.VerticalVelocityParam, playerMovementController.VerticalVelocity);
            _animator.SetBool(PlayerAnimations.IsGroundedParam, playerMovementController.isGrounded);
        }
    }
}