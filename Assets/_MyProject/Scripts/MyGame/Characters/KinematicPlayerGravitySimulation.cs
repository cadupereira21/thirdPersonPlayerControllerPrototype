using System;
using UnityEngine;

namespace _MyProject.Scripts.MyGame.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class KinematicPlayerGravitySimulation : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = this.GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            if (_characterController.isGrounded) return;

            Vector3 gravity = Physics.gravity * Time.fixedDeltaTime;
            _characterController.Move(gravity);
        }
    }
}