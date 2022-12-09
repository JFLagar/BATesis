using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;
using UnityEngine.InputSystem;
namespace SkillIssue.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        public Character character;
        public bool player2;
        public CharacterAI ai;
        public bool aiControl = false;
        public bool controllerControl = false;
        public PlayerInput playerInput;

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
        private bool movementUp, lightUp, heavyUp, specialUp;
        public CommandInputs movement;
        public CommandInputs input;
        public Vector2 direction;
        string horizontal = "Horizontal";
        string vertical = "Vertical";
        // Update is called once per frame
        private void Awake()
        {
            if (aiControl)
                ai.Initiate(this);
        }
        private void Start()
        {
            movementInput.character = character;
            lightButton.character = character;
            heavyButton.character = character;
            specialButton.character = character;
            if (player2)
            {
                horizontal = "Horizontal2";
                vertical = "Vertical2";
            }
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
          

        }
        void Update()
        {                                 
            ///OLD
            ///
            //lightButton.Update();
            //heavyButton.Update();
            //specialButton.Update();
            //    //movement
            //    movement = HandleMovementInput();
            //if (movement != null)
            //{
            //    if (!aiControl)
            //        movementInput.direction = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
            //    else
            //        movementInput.direction = new Vector2(ai.dir.x * character.faceDir, ai.dir.y);
            //    movement.Activate(movementUp);
            //}

            ////attack
            //input = HandleAttackInput();
            //if (input != null)
            //{
            //    input.Activate(AttackUp());               
            //}
            //if (!aiControl)
            //    direction = movementInput.direction;
            //else
            //    direction = ai.dir * character.faceDir;
            //if(Input.GetKeyDown(KeyCode.Comma))
            //{
            //    attackInputs[1].InputPressed();
            //}
        }
      
       public CommandInputs HandleAttackInput()
        {
            if (aiControl)
            {
                switch(ai.attackInput)
                {
                    case 0:
                            return null;
                    case 1:
                        lightUp = ai.buttonUp;
                        return lightButton;
                    case 2:
                        heavyUp = ai.buttonUp;
                        return heavyButton;
                }
            }

            //Light
            if (Input.GetKeyDown(inputs[0]))
            {
                lightUp = false;
                return lightButton; 
            }

            if (Input.GetKeyUp(inputs[0]))
            {
                lightUp = true;
                return lightButton;
            }
            //Heavy
            else if (Input.GetKeyDown(inputs[1]))
            {
                heavyUp = false;
                return heavyButton;
            }
            if (Input.GetKeyUp(inputs[1]))
            {
                heavyUp = true;
                return heavyButton;
            }
            //Special
            else if (Input.GetKeyDown(inputs[2]))
            {
                specialUp = false;
                return specialButton;
            }
            if (Input.GetKeyUp(inputs[2]))
            {
                specialUp = true;
                return specialButton;
            }
            else
            { 
                return null; 
            }          
        }
        public CommandInputs HandleMovementInput()
        {
            if(aiControl)
            {
                if (ai.dir != Vector2.zero)
                    return movementInput;
                else
                    return null;
            }
            if (!controllerControl)
            {
                if (Input.GetButtonDown(horizontal) || Input.GetButtonDown(vertical))
                {
                    movementUp = false;
                    return movementInput;
                }

                if (Input.GetButtonUp(horizontal) || Input.GetButtonUp(vertical))
                {
                    movementUp = true;
                    return movementInput;
                }
                return null;
            }
            else
            {
                horizontal = "ControllerHorizontal";
                vertical = "ControllerVertical";
                if(Input.GetAxisRaw(horizontal) == 0 || Input.GetAxisRaw(vertical)!= 0)
                {
                    movementUp = false;
                    return movementInput;
                }
                else
                {
                    movementUp = true;
                    return movementInput;
                }
            }

        }
        bool AttackUp()
        {
            if (input == lightButton)
            {
                return lightUp;
            }
            if (input == heavyButton)
            {
                return heavyUp;
            }
            if (input == specialButton)
            {
                return specialUp;
            }
            return false;
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
        public void LightButton()
        {
            lightButton.InputPressed();
        }
        public void HeavyButton()
        {
            heavyButton.InputPressed();
        }
        public void SpecialButton()
        {
            specialButton.InputPressed();
        }
     
    }
}
