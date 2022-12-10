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
        public bool player2;
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

            if (DataManagment.instance != null)
            {
                if (!player2)
                { 
                    inputs = DataManagment.instance.data.inputsP1; 
                }
                else
                {
                    //inputs = DataManagment.instance.data.inputsP2;
                }
            }
            if(!player2)
            inputActions = new NewControls();

            MapActions(player2);

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
            if(!player2)
            {
                inputActions.ControlsP1.Enable();
                inputActions.ControlsP1.LightButton.performed += LightButton;
                inputActions.ControlsP1.HeavyButton.performed += HeavyButton;
                inputActions.ControlsP1.SpecialButton.performed += SpecialButton;
                inputActions.ControlsP1.Start.performed += StartButton;
                inputActions.ControlsP1.Select.performed += SelectButton;
                inputActions.ControlsP1.MovementX.performed += MovementXDown;
                inputActions.ControlsP1.MovementX.canceled += MovementXUp;
                inputActions.ControlsP1.MovementY.performed += MovementYDown;
                inputActions.ControlsP1.MovementY.canceled += MovementYUp;
            }
            else
            {
                inputActions.ControlsP2.Enable();
                inputActions.ControlsP2.LightButton.performed += LightButton;
                inputActions.ControlsP2.HeavyButton.performed += HeavyButton;
                inputActions.ControlsP2.SpecialButton.performed += SpecialButton;
                inputActions.ControlsP2.Start.performed += StartButton;
                inputActions.ControlsP2.Select.performed += SelectButton;
                inputActions.ControlsP2.MovementX.performed += MovementXDown;
                inputActions.ControlsP2.MovementX.canceled += MovementXUp;
                inputActions.ControlsP2.MovementY.performed += MovementYDown;
                inputActions.ControlsP2.MovementY.canceled += MovementYUp;
            }
            

            inputActions.Menu.Confirm.performed += Confirm;
            inputActions.Menu.Cancel.performed += Cancel;
            inputActions.Menu.Navigate.performed += NavigateUI;
            
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

        private void MovementXUp(InputAction.CallbackContext context)
        {
            direction.x = 0;
        }

        private void MovementXDown(InputAction.CallbackContext context)
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
            lightButton.InputPressed();
        }
        public void HeavyButton(InputAction.CallbackContext context)
        {
            heavyButton.InputPressed();
        }
        public void SpecialButton(InputAction.CallbackContext context)
        {
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
