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
            MapActions();

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
        public void MapActions()
        {
            NewControls inputActions = new NewControls();
            inputActions.StandardMap.Enable();
            inputActions.StandardMap.LightButton.performed += LightButton;
            inputActions.StandardMap.HeavyButton.performed += HeavyButton;
            inputActions.StandardMap.SpecialButton.performed += SpecialButton;
            inputActions.StandardMap.Start.performed += StartButton;
            inputActions.StandardMap.Select.performed += SelectButton;
            inputActions.StandardMap.MovementX.performed += MovementXDown;
            inputActions.StandardMap.MovementX.canceled += MovementXUp;
            inputActions.StandardMap.MovementY.performed += MovementYDown;
            inputActions.StandardMap.MovementY.canceled += MovementYUp;
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
