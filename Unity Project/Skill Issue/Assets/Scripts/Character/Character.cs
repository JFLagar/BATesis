using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.Inputs;
using SkillIssue.StateMachineSpace;
namespace SkillIssue.CharacterSpace
{
    public class Character : MonoBehaviour, IPhysics, IHitboxResponder
    {
        public Character oponent;
        public float faceDir;
        public float xDiff;
        public SpriteRenderer render;
        public StateMachine stateMachine;
        public InputHandler inputHandler;
        public AttackData[] standingAttacks;
        public AttackData[] crouchingAttacks;
        public AttackData[] jumpAttacks;
        public AttackData[] specialAttacks;
        public AttackClass attack;
        public Animator animator;
        public Pushbox pushbox;
        public bool applyGravity = false;
        public bool wall;
        public float wallx;
        public bool isGrounded;
        public float x;
        public float y;

        [Space]

        public float movementspeed;
        public float jumpPower;
        public float forceSpeed;
        public float forceLeftOver;

        [Space]

        public Transform collisions;
   
        private void Awake()
        {
            inputHandler.character = this;
            stateMachine.character = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            pushbox.setResponder(this);
            pushbox.character = this;
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.StateMachineUpdate();
            if (oponent == null)
                return;
            xDiff = transform.position.x - oponent.transform.position.x;
            if ( xDiff < 0)
            {
                faceDir = 1;
                if (render != null)
                 render.flipX = false;
                collisions.eulerAngles = new Vector3(0, 0, 0);
                         
            }
            else
            {
                faceDir = -1;
                if (render != null) render.flipX = true;
                collisions.eulerAngles = new Vector3(0, 180, 0);
            }
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
                            attack.Attack(standingAttacks[((int)type)]);
                        }
                        break;
                    case CrouchState:
                        attack.Attack(crouchingAttacks[((int)type)]);
                        break;
                    case JumpState:
                        attack.Attack(jumpAttacks[((int)type)]);
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
        public void FixPosition()
        {
            
            transform.position.Set(transform.position.x, 0, 0);

        }
        public void CharacterMove(Vector2 direction)
        {
            float speed = movementspeed;
            x = direction.x;
            if (x != 0)
            {
                if (x != faceDir)
                {
                    animator.SetInteger("X", -1);
                    speed = movementspeed / 2;
                }

                else
                {
                    animator.SetInteger("X", 1);
                    speed = movementspeed;
                }
            }
            else
                animator.SetInteger("X", 0);           

         
            if (wall)
            {
                if (direction.x == 0 || direction.x == wallx)
                    transform.Translate(new Vector2(0, 0) * speed * Time.deltaTime);
                else
                {
                    wall = false;
                    transform.Translate(new Vector2(direction.x, 0) * speed * Time.deltaTime);
                }
            }    
           else
            transform.Translate(new Vector2(direction.x, 0) * speed * Time.deltaTime);

        }
        public void CharacterPush(float x)
        {
            if(!wall)
            transform.Translate(new Vector2(x, 0) * Time.deltaTime);
        }
        public void ApplyForce(Vector2 direction, float duration)
        {
            if (wall && direction.x == wallx)
                direction.x = 0;
            y = direction.y;
            applyGravity = false;

                StopAllCoroutines();
                StartCoroutine(ForceCoroutine(direction, duration));
    
        }

        public void ApllyGravity()
        {
            if (!applyGravity)
                return;
            if(stateMachine.currentAction == ActionStates.None)
            {
                animator.SetTrigger("JumpEnd");
            }
            if (!wall)
            transform.Translate(new Vector2(x, -1) * (forceSpeed/2) * Time.deltaTime);
            else
            transform.Translate(new Vector2(0, -1) * (forceSpeed / 2) * Time.deltaTime);
        }
        public IEnumerator ForceCoroutine(Vector2 direction, float duration)
        {
            float i = 0f;
            while(i != duration)
            {
                if (wall)
                direction.x = 0;
                x = direction.x;
                transform.Translate(direction * forceSpeed* Time.deltaTime);
                yield return null;
                i++;
                forceLeftOver = duration - i;
            }
            applyGravity = true;
        }
        public void IsGrounded(bool check)
        {
            isGrounded = check;
        }

        public void CollisionedWith(Collider2D collider)
        {
            if (collider == GetComponent<Collider2D>())
                return;
            Pushbox collidedbox = collider.GetComponent<Pushbox>();
            collidedbox?.HandleCollision(pushbox);
        
        }
        public void SetWall(bool isWall, int x)
        {
            wall = isWall;
            wallx = x;
        }
     
        public void CheckState()
        {
            Debug.Log(animator.GetCurrentAnimatorClipInfo(0));
        }
    }
}
