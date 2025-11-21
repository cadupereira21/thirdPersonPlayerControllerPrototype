using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _MyProject.Scripts.MyGame.Input
{
    public class InputManager : MonoBehaviour
    {
        private InputControl _controls;
        
        public InputControl.PlayerActions PlayerActions => _controls.Player;
        
        public InputControl.UIActions UIActions => _controls.UI;
        
        public static InputManager Instance { get; private set; }
        
        private void Awake()
        {
            Debug.Log("[InputManager] Awake");
            _controls = new InputControl();
            
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }
    }
}
