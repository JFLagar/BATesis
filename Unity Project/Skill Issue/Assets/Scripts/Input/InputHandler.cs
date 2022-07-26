using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkillIssue.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        public List<string> commands = new List<string>();
        private LightInput lightButton = new LightInput();
        private HeavyInput heavyButton = new HeavyInput();
        private SpecialInput specialButton = new SpecialInput();
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            ICommandInput input = HandleInput();
            if (input != null)
            { input.InputPressed();
                commands.Add(input.ToString());
            }
        }

        ICommandInput HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A)) { return lightButton; }
            else if (Input.GetKeyDown(KeyCode.S)){ return heavyButton; }
            else if (Input.GetKeyDown(KeyCode.D)) { return specialButton; }
            else { return null; }
            
        }
    }
}
