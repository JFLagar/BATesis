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
    public enum States
    {
        Standing,
        Crouching,
        Jumping
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
            standingState.character = character;
            crouchingState.character = character;
            jumpState.character = character;
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
        public Character character;
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
        bool action;
        private float yvalue;
        public override void Update(InputHandler input)
        {
            if (!stateMachine.character.isGrounded)
            {
                yvalue = 1;
                ExitState();
            }
            action = (character.stateMachine.currentAction == ActionStates.None || character.stateMachine.currentAction == ActionStates.Hit);
            if (!action)
            {
                return;
            }
            if (character.stateMachine.currentAction == ActionStates.None)
            {
                if (input.movementInput.direction.y != 0)
                {
                    yvalue = input.movementInput.direction.y;
                    if (yvalue > 0)
                        stateMachine.character.ApplyForce(input.direction, stateMachine.character.jumpPower);
                    //jump
                    ExitState();
                }
                stateMachine.character.CharacterMove(input.direction);
            }
                      
        }
        public override void EnterState() 
        {
            stateMachine.character.applyGravity = false;
            stateMachine.currentState = stateMachine.standingState;
            stateMachine.character.currentState = States.Standing;
          
        }
        public override void ExitState() {

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
        bool action;
        public override void Update(InputHandler input)
        {
            action = character.stateMachine.currentAction == ActionStates.None;
            if (!action)
            {
                return;
            }
            if (input.movementInput.direction.y != -1)
            { 
                ExitState();
            }
            //Gets Input for blocking
            stateMachine.character.CharacterMove(input.movementInput.direction);
        }
        public override void EnterState() 
        {

            stateMachine.currentState = stateMachine.crouchingState;
            stateMachine.character.currentState = States.Crouching;
            character.animator.Play("StandToCrouch");
            character.animator.SetBool("Crouching", true);
        }
        public override void ExitState() 
        {

            stateMachine.standingState.EnterState();
            character.animator.SetBool("Crouching", false);
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
            stateMachine.character.isGrounded = false;
            stateMachine.currentState = stateMachine.jumpState;
            stateMachine.character.currentState = States.Jumping;
            if(stateMachine.currentAction == ActionStates.None)
                character.animator.Play("JumpStart");
            character.animator.SetBool("Jumping", true);
        }
        public override void ExitState() 
        {
            stateMachine.character.FixPosition();
            stateMachine.standingState.EnterState();
            character.animator.SetBool("Jumping", false);
        }
    }
}