using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
using SkillIssue.StateMachineSpace;
namespace SkillIssue.CharacterSpace
{
    public class Character : MonoBehaviour, IPhysics
    {
        public StateMachine stateMachine;
        public InputHandler inputHandler;
        public AttackData[] standingAttacks;
        public AttackData[] crouchingAttacks;
        public AttackData[] jumpAttacks;
        public AttackData[] specialAttacks;
        public bool applyGravity = false;
        public bool isGrounded;
        private float x;
        private void Awake()
        {
            inputHandler.character = this;
            stateMachine.character = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.StateMachineUpdate();
        }
        public void PerformAttack(AttackType type)
        {
            stateMachine.currentAction = ActionStates.Attack;
            if ((int)type != 2)
            {
                switch (stateMachine.currentState)
                {
                    case StandingState:
                        if (inputHandler.movementInput.direction.x > 0)
                        {
                            Debug.Log(standingAttacks[((int)type + (int)inputHandler.movementInput.direction.x) + 1].ToString());
                        }
                        else
                        {
                            Debug.Log(standingAttacks[((int)type)].ToString());
                        }
                        break;
                    case CrouchState:
                        Debug.Log(crouchingAttacks[((int)type)].ToString());
                        break;
                    case JumpState:
                        Debug.Log(jumpAttacks[((int)type)].ToString());
                        break;
                }
            }
            else
            {
                switch (stateMachine.currentState)
                {
                    case StandingState:
                        if (inputHandler.movementInput.direction.x != 0)
                        {
                            Debug.Log(specialAttacks[(int)inputHandler.movementInput.direction.x + 1].ToString());
                        }
                        else
                        {
                            Debug.Log(specialAttacks[1].ToString());
                        }
                        break;
                    case CrouchState:
                        if (inputHandler.movementInput.direction.x != 0)
                        {
                            Debug.Log(specialAttacks[(int)inputHandler.movementInput.direction.x + 1].ToString());
                        }
                        else
                        {
                            Debug.Log(specialAttacks[3].ToString());
                        }
                        break;
                    case JumpState:
                        Debug.Log(specialAttacks[4].ToString());
                        break;
                }
            }
        }

        public void ApplyForce(Vector2 direction, float duration)
        {
            applyGravity = false;
            StopCoroutine(ForceCoroutine(direction, duration));
            StartCoroutine(ForceCoroutine(direction, duration));
        }

        public void ApllyGravity()
        {
            if (!applyGravity)
                return;
            transform.Translate(new Vector2(x, -1) * Time.deltaTime);
        }
        public IEnumerator ForceCoroutine(Vector2 direction, float duration)
        {
            x = direction.x;
            float i = 0f;
            while(i != duration)
            {
                transform.Translate(direction * Time.deltaTime);
                yield return null;
                i++;
            }
            applyGravity = true;
        }
        public void IsGrounded(bool check)
        {
            isGrounded = check;
        }
    }
}
