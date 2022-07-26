using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
using SkillIssue.StateMachineSpace;
namespace SkillIssue.CharacterSpace
{
    public class Character : MonoBehaviour
    {
        public StateMachine stateMachine;
        public InputHandler inputHandler;
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

        }
        public void PerformAttack(AttackType type)
        {
            Debug.Log("Performing an Attack: " + type);
            stateMachine.currentAction = ActionStates.Attack;
        }
    }
}
