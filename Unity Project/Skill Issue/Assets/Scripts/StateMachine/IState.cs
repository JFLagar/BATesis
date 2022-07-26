using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkillIssue.StateMachine
{
    public interface IState
    {
    public void EnterState() { }
    public void ExitState() { }
    }
}
