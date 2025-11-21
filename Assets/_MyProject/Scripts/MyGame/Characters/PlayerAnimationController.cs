using System;
using UnityEngine;

namespace _MyProject.Scripts.MyGame.Characters
{
    public abstract class PlayerAnimations
    {
        public static readonly int VelocityParam = Animator.StringToHash("Velocity");
        public static readonly int JumpingParam = Animator.StringToHash("IsJumping");
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
                _animator.SetFloat(PlayerAnimations.VelocityParam, playerMovementController.HorizontalVelocity);
            }
            
            _animator.SetBool(PlayerAnimations.JumpingParam, playerMovementController.IsJumping);
        }
    }
}