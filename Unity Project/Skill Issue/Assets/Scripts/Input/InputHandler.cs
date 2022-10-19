using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;
namespace SkillIssue.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        public Character character;
        public bool player2;
        [SerializeField]
        public List<string> commands = new List<string>();
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

        }
        void Update()
        {
            lightButton.Update();
            heavyButton.Update();
            specialButton.Update();
            //movement
            movement = HandleMovementInput();
            if (movement != null)
            {
                movementInput.direction = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
                movement.Activate(movementUp);
                commands.Add(movement.ToString());
            }

            if (inputs.Length == 0)
                return;
            //attack
            input = HandleAttackInput();
            if (input != null)
            {
                input.Activate(AttackUp());
                commands.Add(input.ToString());
            }
            direction = movementInput.direction;
        }

        CommandInputs HandleAttackInput()
        {
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
        CommandInputs HandleMovementInput()
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
            else { return null; }

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
     
    }
}
