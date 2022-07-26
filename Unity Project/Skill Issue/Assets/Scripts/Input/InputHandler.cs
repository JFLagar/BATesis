using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkillIssue.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        public List<string> commands = new List<string>();
        public KeyCode[] inputs;
        private LightInput lightButton = new LightInput();
        private HeavyInput heavyButton = new HeavyInput();
        private SpecialInput specialButton = new SpecialInput();
        public MovementInput movementInput = new MovementInput();
        // Start is called before the first frame update

        public CommandInputs movement;
        public CommandInputs input;
        // Update is called once per frame
        void Update()
        {
            movement = HandleMovementInput();
            if (movement != null)
            {
                movementInput.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                movement.InputPressed();
                commands.Add(movement.ToString());
            }
            input = HandleAttackInput();
            if (input != null)
            { input.InputPressed();
                commands.Add(input.ToString());
            }

        }

        CommandInputs HandleAttackInput()
        {
            if (Input.GetKeyDown(inputs[0])) { return lightButton; }
            else if (Input.GetKeyDown(inputs[1])){ return heavyButton; }
            else if (Input.GetKeyDown(inputs[2])) { return specialButton; }
            else { return null; }
            
        }
        CommandInputs HandleMovementInput()
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) { return movementInput; }
            else { return null; }

        }
    }
}
