using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
namespace SkillIssue.StateMachine
{
    public enum ActionStates
    {
        None, // Default state
        Attack, //Can go back to None or proper Attack, getting hit here will trigger counterhit
        Block, //Goes back to None
        Hit //King = Overrides all States and Goes back to None
    }
    public class StateMachine : MonoBehaviour
    {
        public State standingState = new StandingState();
        public State crouchingState = new CrouchState();
        public State jumpState = new JumpState();
        public State currentState;
        public ActionStates currentAction = 0;
        [SerializeField]
        InputHandler inputHandler;
        private void Awake()
        {
            standingState.stateMachine = this;
            crouchingState.stateMachine = this;
            jumpState.stateMachine = this;
        }
        void Start()
        {

            currentState = standingState;
            currentState.stateMachine = this;
            currentState.EnterState();
        }
        // Update is called once per frame
        void Update()
        {
            if(currentState.stateMachine == null)
            {
                currentState.stateMachine = this;
            }
            currentState.Update(inputHandler);
        }

    }

    //Abstract class
    public class State : IState
    {
        public StateMachine stateMachine;
        public virtual void Update(InputHandler input)
        { }
        public virtual void EnterState()
        {
        }
        public virtual void ExitState()
        {
        }
    }

    public class StandingState : State
    {
        private float yvalue;
        public override void Update(InputHandler input)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            { 
                Debug.Log("GroundState");
                stateMachine.currentAction = ActionStates.Attack;
            }
            if (input.movementInput.direction.y != 0)
            {
                yvalue = input.movementInput.direction.y;
                ExitState(); }
        }
        public override void EnterState() 
        { 
            Debug.Log("Entering GroundState");
            stateMachine.currentState = stateMachine.standingState;
        }
        public override void ExitState() { 
            Debug.Log("Exiting GroundState");
            if (yvalue == 1)
            {
                stateMachine.jumpState.EnterState();
            }
            else
            {
                stateMachine.crouchingState.EnterState();
            }
        }
    }
    public class CrouchState : State
    {

        public override void Update(InputHandler input)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { Debug.Log("CrouchState"); }
            if (input.movementInput.direction.y != -1)
            { 
                ExitState();
            }
        }
        public override void EnterState() 
        {
            Debug.Log("Entering CrouchState");
            stateMachine.currentState = stateMachine.crouchingState;
        }
        public override void ExitState() 
        {
            Debug.Log("Exiting CrouchState");
            stateMachine.standingState.EnterState();
        }
    }
    public class JumpState : State
    {
        public override void Update(InputHandler input)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { Debug.Log("JumpState"); }
        }
        public override void EnterState()
        {
            Debug.Log("Entering JumpState");
            stateMachine.currentState = stateMachine.jumpState;
        }
        public override void ExitState() 
        { 
            Debug.Log("Exiting JumpState");
            stateMachine.standingState.EnterState();
        }
    }
}