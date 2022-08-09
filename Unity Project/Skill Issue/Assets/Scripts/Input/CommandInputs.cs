using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;
namespace SkillIssue.Inputs
{
    //Template
    public class CommandInputs : ICommandInput
    {
        public Character character;
        private bool pressed = false;
        private float buttonHeld;
        public void Update()
        {
            if(pressed)
            {
                buttonHeld += (0.1f * Time.deltaTime);
            }
            if (buttonHeld >= 0.1)
            {
                InputHold(buttonHeld);
            }

        }
        public void Activate(bool isUp)
        {
            if (pressed == false)
            {
                InputPressed();
                pressed = true;
            }
            if (isUp == true)
            {
                buttonHeld = 0;
                pressed = false;
                InputReleased();
            }

        }
        public virtual void InputPressed() { }
        public virtual void InputReleased() { }
        public virtual void InputHold(float time) { }
    }

    public class LightInput : CommandInputs
    {
        public override void InputPressed() 
        {
            character.PerformAttack(AttackType.Light);
        }
        public override void InputReleased() 
        {
            Debug.Log("LightReleased");
        }
        public override void InputHold(float time)
        {
            Debug.Log("LightHold");
        }
    }
    public class HeavyInput : CommandInputs
    {
        public override void InputPressed() 
        { 
            character.PerformAttack(AttackType.Heavy);
        }
        public override void InputReleased() 
        {
            Debug.Log("HeavyReleased");
        }
        public override void InputHold(float time) 
        {
            Debug.Log("HeavyHold");
        }
    }
    public class SpecialInput : CommandInputs
    {
        public override void InputPressed() 
        {
            character.PerformAttack(AttackType.Special);
        }
        public override void InputReleased() 
        { 
        
        }
        public override void InputHold(float time) 
        {
        
        }
    }
    public class MovementInput : CommandInputs
    {
        public Vector2 direction;
        public override void InputPressed()
        {

        }
        public override void InputReleased() 
        {
            direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        }
        public override void InputHold(float time)
        {

        }
    }
}

