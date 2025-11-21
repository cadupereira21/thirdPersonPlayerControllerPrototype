using System;
using UnityEngine;

namespace _MyProject.Scripts.MyGame.Characters
{
    public sealed class PlayerAnimations
    {
        public static readonly int VelocityParam = Animator.StringToHash("Velocity");
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
        }
    }
}