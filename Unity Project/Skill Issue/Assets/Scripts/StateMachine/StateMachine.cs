using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
using SkillIssue.CharacterSpace;
namespace SkillIssue.StateMachineSpace
{
    public enum ActionStates
    {
        None, // Default state
        Attack, //Can go back to None or proper Attack, getting hit here will trigger counterhit
        Block, //Goes back to None
        Hit //King = Overrides all States and Goes back to None
    }
    public class StateMachine: MonoBehaviour
    {
        public Character character;

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

        private void Start()
        {
        currentState = jumpState;
        currentState.stateMachine = this;
        currentState.EnterState();
    }

        // Update is called once per frame
        public void StateMachineUpdate()
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

            if (input.movementInput.direction.y != 0)
            {
                yvalue = input.movementInput.direction.y;
                if (yvalue > 0)
                    stateMachine.character.ApplyForce(input.movementInput.direction, stateMachine.character.jumpPower) ;
                ExitState();
            }
            if (!stateMachine.character.isGrounded)
            {
                yvalue = 1;
                ExitState();
            }
            stateMachine.character.CharacterMove(input.movementInput.direction);
        }
        public override void EnterState() 
        { 
            Debug.Log("Entering GroundState");
            stateMachine.currentState = stateMachine.standingState;
            stateMachine.character.applyGravity = false;
            stateMachine.character.FixPosition();
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
           
            stateMachine.character.ApllyGravity();
            if (stateMachine.character.isGrounded)
                ExitState();

        }
        public override void EnterState()
        {
            Debug.Log("Entering JumpState");
            stateMachine.character.isGrounded = false;
            stateMachine.currentState = stateMachine.jumpState;
        }
        public override void ExitState() 
        { 
            Debug.Log("Exiting JumpState");
            stateMachine.standingState.EnterState();
        }
    }
}