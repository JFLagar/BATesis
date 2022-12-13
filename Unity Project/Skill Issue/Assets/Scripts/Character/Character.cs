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
        public SpriteRenderer vfx;
        public SpriteRenderer screenCheck;
        public StateMachine stateMachine;
        public InputHandler inputHandler;
        public AttackData[] standingAttacks;
        public AttackData[] crouchingAttacks;
        public AttackData[] jumpAttacks;
        public AttackData[] specialAttacks;
        public AttackClass attack;
        public Animator animator;
        public CharacterAnimationManagar characterAnimation;
        public Pushbox pushbox;
        public bool applyGravity = false;
        public bool wall;
        public bool cameraWall = false;
        public float wallx;
        public bool isGrounded;
        public float x;
        public float y;
        public ActionStates currentAction;
        public States currentState;
        [Space]

        public int maxHealth;
        public int currentHealth;
        public int comboHit;

        [Space]

        public float movementspeed;
        public float jumpPower;
        public float forceSpeed;
        public float forceLeftOver;
        bool landed;
        bool landingRecovery;
        public bool isJumping;
        [Space]

        public Transform collisions;
        private Coroutine currentHitCoroutine;
        private Coroutine currentMovementCoroutine;
        public AttackData storedAttack = null;
        public List<AttackData> currentCombo = new List<AttackData>();
        public bool visualState;
        public Color32[] stateColors;

        private Transform origin;
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
            origin = transform;
        }

        // Update is called once per frame
        void Update()
        {
    
            currentAction = stateMachine.currentAction;
            stateMachine.StateMachineUpdate();
            cameraWall = !screenCheck.isVisible;
            if (!screenCheck.isVisible)
            {
                x = 0;
                wallx = -faceDir;
            }
            if (oponent == null)
                return;
            xDiff = transform.position.x - oponent.transform.position.x;
            if(currentState != States.Jumping)
            {
                if (xDiff < 0)
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
                vfx.flipX = render.flipX;
            }
            comboHit = currentCombo.Count;
            //Safety messure against stunlock
            if(currentHitCoroutine == null && currentAction == ActionStates.Hit)
            {
                currentHitCoroutine = StartCoroutine(RecoveryFramesCoroutines(10));
            }
            if(visualState)
            {
                switch (currentAction)
                {
                    case ActionStates.None:
                        render.color = stateColors[0];
                        break;
                    case ActionStates.Hit:
                        render.color = stateColors[1];
                        break;
                }
            }

        }
        public void PerformAttack(AttackType type)
        {
            //here comes the canceable attack
            if ((int)type != 2)
            {
                switch (stateMachine.currentState)
                {
                    case StandingState:
                        //if (inputHandler.movementInput.direction.x > 0)
                        //{
                        //    Debug.Log(standingAttacks[((int)type + (int)inputHandler.movementInput.direction.x) + 1].ToString());
                        //    return;
                        //}
                        //else
                        {
                            switch (inputHandler.direction.y)
                            {
                                case 0f:
                                    attack.Attack(standingAttacks[((int)type)]);
                                    break;
                                case 1f:
                                    ApplyForce(inputHandler.direction, jumpPower);
                                    attack.Attack(jumpAttacks[((int)type)]);
                                    break;
                                case -1f:
                                    attack.Attack(crouchingAttacks[((int)type)]);
                                    break;
                            }
                            
                        }
                        break;

                    case CrouchState:
                        switch (inputHandler.direction.y)
                        {
                            case 0f:
                                attack.Attack(standingAttacks[((int)type)]);
                                break;
                            case -1f:
                                attack.Attack(crouchingAttacks[((int)type)]);
                                break;
                        }
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
                        if (inputHandler.direction.x != 0)
                        {
                            int id = ((int)(inputHandler.direction.x * faceDir));
                            attack.Attack(specialAttacks[id + 2]);
                            break;
                        }
                        else
                        {
                            attack.Attack(specialAttacks[2]);
                            break;
                        }
                    case CrouchState:
                        if (inputHandler.direction.x != 0)
                        {
                            int id = ((int)(inputHandler.direction.x * faceDir));
                            attack.Attack(specialAttacks[id + 2]);
                            break;
                        }
                        else
                        {
                            attack.Attack(specialAttacks[2]);
                            break;
                        }
                    case JumpState:
                        break;
                }
            }
          
        }
        public void GetHit(AttackData data, bool blockCheck = false)
        {

            if (inputHandler.direction.x == -faceDir && currentAction != ActionStates.Hit && IsBlocking(data.attackAttribute))
            {
                BlockDone(data, blockCheck);

            }
           else if (currentAction == ActionStates.Attack)
            {
                DamageDealt(data);
            }
            //block
            else if(!blockCheck)
            {
                DamageDealt(data);
            }                     
        }
        private void DamageDealt(AttackData data)
        {
            Debug.Log(data.name);
            Vector2 dir = new Vector2(data.push.x * -faceDir, 0);

            stateMachine.currentAction = ActionStates.Hit;
            if (data.launcher)
            {
                characterAnimation.AddAnimation(AnimType.Hit, "JumpingHit");
                stateMachine.currentState = stateMachine.jumpState;
                dir.y = data.push.y;
            }
            else
            {
                characterAnimation.AddAnimation(AnimType.Hit, currentState.ToString() + "Hit");
            }
            if (currentHitCoroutine != null)
                StopCoroutine(currentHitCoroutine);
            currentHitCoroutine = StartCoroutine(RecoveryFramesCoroutines(data.hitstun));
            currentHealth = currentHealth - data.damage;
            if (wall)
            {
                ApplyCounterPush(-dir, 3f);
            }
            else
                ApplyForce(dir, 3f);
        }
        private void BlockDone(AttackData data, bool blockCheck = false)
        {
            Vector2 dir = new Vector2(data.push.x * -faceDir, 0);
            Vector2 blockDir = new Vector2(dir.x, 0);
            stateMachine.currentAction = ActionStates.Block;
            characterAnimation.AddAnimation(AnimType.Hit, currentState.ToString() + "Block");
            Debug.Log("Blocked");
            if (currentHitCoroutine != null)
                StopCoroutine(currentHitCoroutine);
            if (blockCheck)
                currentHitCoroutine = StartCoroutine(RecoveryFramesCoroutines(data.blockstun / 2));
            else
            {
                currentHitCoroutine = StartCoroutine(RecoveryFramesCoroutines(data.blockstun));
                if (wall)
                {
                    ApplyCounterPush(-blockDir, 3f);
                }
                else
                    ApplyForce(blockDir, 3f);
            }
        }
        public void ResetPos()
        {
            transform.position = origin.position;
        }
        public void FixPosition()
        {
            if (!landed)
            {
                transform.position = new Vector3(transform.position.x, 0f, 0);
                landed = true;
            }
        }

        public void CharacterMove(Vector2 direction)
        {
            float speed = movementspeed;
            x = direction.x;
            if (stateMachine.currentAction != ActionStates.None || currentState != States.Standing)
                return;
        
            if (x != 0)
            {
                if (x != faceDir)
                {
                    animator.SetInteger("X", -1);
                    speed = movementspeed / 1.3f;
                }

                else
                {
                    animator.SetInteger("X", 1);
                    speed = movementspeed;
                }
            }
            else
                animator.SetInteger("X", 0);           

         
            if (wall || cameraWall)
            {
                if (direction.x == 0 || direction.x == wallx)

                {
                    transform.Translate(new Vector2(0, 0) * speed * Time.deltaTime);
                }
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
            if (wall && x == wallx || x == 0)
                return;
            transform.Translate(new Vector2(x, 0) * Time.deltaTime);
            wall = false;
        }
        public void ApplyCounterPush(Vector2 direction, float duration)
        {
            oponent.wall = false;
            oponent.ApplyForce(direction, duration, true);
        }
        public void ApplyForce(Vector2 direction, float duration, bool counterforce = false)
        {
            bool m_bool = false;
            if (counterforce)
            {
                y = 0;
                m_bool = counterforce;
            }           
            else
            {
                if (wall && direction.x == wallx || cameraWall && direction.x == wallx)
                    direction.x = 0;
                else
                    wall = false;
                y = direction.y;
                applyGravity = false;
            }
       

            if(currentMovementCoroutine != null)
                StopCoroutine(currentMovementCoroutine);
              
            currentMovementCoroutine = StartCoroutine(ForceCoroutine(direction, duration, m_bool));
    
        }
        public void ApplyAttackForce(AttackData data)
        {
            Vector2 direction = data.movement;
            float duration = data.movementDuration;
            {
                if (wall && direction.x == wallx || cameraWall && direction.x == wallx)
                    direction.x = 0;
                else
                    wall = false;
                y = direction.y;
                applyGravity = false;
            }


            if (currentMovementCoroutine != null)
                StopCoroutine(currentMovementCoroutine);

            currentMovementCoroutine = StartCoroutine(ForceCoroutine(direction * faceDir, duration, false));

        }

        public void ApllyGravity()
        {
            if (!applyGravity)
                return;
            if (stateMachine.currentAction == ActionStates.None && isJumping)
            {
                characterAnimation.AddAnimation(AnimType.Movement, "JumpFall");
            }
            if(!isGrounded)
            isJumping = false;
            landed = false;
           
            if ((wall && x == wallx) || (cameraWall && x == wallx))
                transform.Translate(new Vector2(0, -1) * (forceSpeed) * Time.deltaTime);
            else
            {
                transform.Translate(new Vector2(x, -1) * (forceSpeed) * Time.deltaTime);
            }
            
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
        public void AnimEnd()
        {
            oponent.ResetAttackInfo();
            characterAnimation.ClearAnimations();
            if (stateMachine.currentAction == ActionStates.None)
                return;
            stateMachine.currentAction = ActionStates.None;

        }
        public void OpenHitboxes(int number)
        {
            for (int i = 0; i < number; i++)
            {
                attack.hitboxes[i].state = ColliderState.Open;
            }
        }
        public void HitRecover()
        {
            animator.SetTrigger("Recovery");
        }
        public IEnumerator ForceCoroutine(Vector2 direction, float duration, bool counterForce)
        {
            float i = 0f;
            while (i != duration)
            {
                if(!counterForce)
                {
                    if (direction.x != 0 && ((wall && direction.x == wallx) || (cameraWall && direction.x == wallx)))
                        direction.x = 0;
                }               
                x = direction.x;
                transform.Translate(direction * forceSpeed * Time.deltaTime);
                yield return null;
                i++;
                forceLeftOver = duration - i;
            }
            applyGravity = true;
        }
        public IEnumerator RecoveryFramesCoroutines(int frames)
        {
            int i = 0;
                while(i != frames)
            {
                yield return null;
                i++;
            }
            HitRecover();
        }
        public void TestAction(string name)
        {
            stateMachine.currentAction = ActionStates.Landing;
            Debug.Log(stateMachine.currentAction);
        }
        public void HitboxesEnabled()
        {
            visualState = !visualState;
        }
        public void HitConnect(AttackData data)
        {
            if (oponent.currentAction == ActionStates.Block)
            {
                currentCombo.Clear();
            }
            else
            {
                currentCombo.Add(data);
            }
        }
        public void ResetAttackInfo()
        {
            currentCombo.Clear();
            storedAttack = null;
        }
        bool IsBlocking(AttackAttribute attack)
        {
            switch(currentState)
            {
                case States.Standing:
                    if (attack == AttackAttribute.Low)
                        return false;
                    else
                        return true;
                case States.Crouching:
                    if (attack == AttackAttribute.High)
                        return false;
                    else
                        return true;
                case States.Jumping:
                    return false;
            }
            return false;
        }
    }
}
