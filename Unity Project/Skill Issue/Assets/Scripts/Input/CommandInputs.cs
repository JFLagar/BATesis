using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.StateMachine;
namespace SkillIssue.Inputs
{
    //Template
    public class CommandInputs : ICommandInput
    {
        public virtual void InputPressed() { }
        public virtual void InputReleased() { }
        public virtual void InputHold(float time) { }
    }

    public class LightInput : CommandInputs
    {
        public override void InputPressed() { Debug.Log("Light"); }
        public override void InputReleased() { }
        public override void InputHold(float time) { }
    }
    public class HeavyInput : CommandInputs
    {
        public override void InputPressed() { Debug.Log("Heavy"); }
        public override void InputReleased() { }
        public override void InputHold(float time) { }
    }
    public class SpecialInput : CommandInputs
    {
        public override void InputPressed() { Debug.Log("Special"); }
        public override void InputReleased() { }
        public override void InputHold(float time) { }
    }
    public class MovementInput : CommandInputs
    {
        public Vector2 direction;
        public override void InputPressed() { Debug.Log("pos:" + direction); }
        public override void InputReleased() { }
        public override void InputHold(float time) { }
    }
}

