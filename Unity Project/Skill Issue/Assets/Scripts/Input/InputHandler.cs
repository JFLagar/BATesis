using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;
using UnityEngine.InputSystem;
using System;

namespace SkillIssue.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        public Character character;
        public CharacterAI ai;
        public bool aiControl = false;
        public bool controllerControl = false;
        private PlayerInput playerInput;
        NewControls inputActions;
        [SerializeField]
        public List<CommandInputs> directionInputs = new List<CommandInputs>();
        [SerializeField]
        public List<CommandInputs> attackInputs = new List<CommandInputs>();
        public KeyCode[] inputs;
        private LightInput lightButton = new LightInput();
        private HeavyInput heavyButton = new HeavyInput();
        private SpecialInput specialButton = new SpecialInput();
        public MovementInput movementInput = new MovementInput();
        // Start is called before the first frame update
        public CommandInputs movement;
        public CommandInputs input;
        public Vector2  direction = Vector2.zero;

        // Update is called once per frame
        private void Awake()
        {
            if (aiControl)
                ai.Initiate(this);
            playerInput = GetComponent<PlayerInput>();
    }
        private void Start()
        {
            movementInput.character = character;
            lightButton.character = character;
            heavyButton.character = character;
            specialButton.character = character;


            inputActions = new NewControls();
            playerInput.SwitchCurrentControlScheme(playerInput.defaultControlScheme, Keyboard.current);
            //MapActions(true);
            foreach (InputDevice device in InputSystem.devices)
            {
                Debug.Log(device.displayName + device.deviceId + device.GetType().Name);
            }
        }
        void Update()
        {

        }
      
        public void ResetAI()
        {
            if(!aiControl)
            {
                aiControl = true;
                ai.Initiate(this);
            }
            else
            {
                aiControl = false;
                ai.AiReset();            
            }
            movementInput.direction = Vector2.zero;
        }
        public void MapActions(bool player)
        {
            {
                inputActions.Controls.Enable();
                inputActions.Controls.LightButton.performed += LightButton;
                inputActions.Controls.HeavyButton.performed += HeavyButton;
                inputActions.Controls.SpecialButton.performed += SpecialButton;
                inputActions.Controls.Start.performed += StartButton;
                inputActions.Controls.Select.performed += SelectButton;
                inputActions.Controls.MovementX.performed += MovementXDown;
                inputActions.Controls.MovementX.canceled += MovementXUp;
                inputActions.Controls.MovementY.performed += MovementYDown;
                inputActions.Controls.MovementY.canceled += MovementYUp;
            }
            
        }

        private void NavigateUI(InputAction.CallbackContext obj)
        {
            throw new NotImplementedException();
        }

        private void Cancel(InputAction.CallbackContext obj)
        {
            throw new NotImplementedException();
        }

        private void Confirm(InputAction.CallbackContext obj)
        {
            throw new NotImplementedException();
        }

        public void MovementXUp(InputAction.CallbackContext context)
        {
            direction.x = 0;
        }

        public void MovementXDown(InputAction.CallbackContext context)
        {
            float value = context.ReadValue<float>();
            switch (value)
            {
                case 0:
                    direction.x = 0;
                    break;
                case < 0:
                    direction.x = -1;
                    break;
                case > 0:
                    direction.x = 1;
                    break;
            }
        }

        public void MovementYUp(InputAction.CallbackContext context)
        {
            direction.y = 0;
        }
        public void MovementYDown(InputAction.CallbackContext context)
        {
            float value = context.ReadValue<float>();
           switch (value)
            {
                case 0:
                    direction.y = 0;
                    break;
                case < 0:
                    direction.y = -1;
                    break;
                case > 0:
                    direction.y = 1;
                    break;
            }
        }

        public void LightButton(InputAction.CallbackContext context)
        {
            if (context.started)
                lightButton.InputPressed();
        }
        public void HeavyButton(InputAction.CallbackContext context)
        {
            if (context.started)
                heavyButton.InputPressed();
        }
        public void SpecialButton(InputAction.CallbackContext context)
        {
            if (context.started)
                specialButton.InputPressed();
        }

        public void MovementUp(InputAction.CallbackContext context)
        {
            direction = Vector2.zero;
        }
        public void StartButton(InputAction.CallbackContext context)
        {
            GameManager.instance.EnableTrainingMode();
        }
        public void SelectButton(InputAction.CallbackContext context)
        {
            GameManager.instance.ResetRound();
        }
    }
}
