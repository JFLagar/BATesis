using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
namespace SkillIssue.Inputs
{
    //Template
    public class CommandInputs : ICommandInput
    {
        public void InputPressed() { }
        public void InputReleased() { }
        public void InputHold(float time) { }
    }
}
    public class LightInput : ICommandInput
    {
    public void InputPressed() { Debug.Log("Light"); }
    public void InputReleased() { }
    public void InputHold(float time) { }
}
    public class HeavyInput : ICommandInput
    {
    public void InputPressed() { Debug.Log("Heavy"); }
    public void InputReleased() { }
    public void InputHold(float time) { }
}
    public class SpecialInput : ICommandInput
    {
    public void InputPressed() { Debug.Log("Special"); }
    public void InputReleased() { }
    public void InputHold(float time) { }
}
