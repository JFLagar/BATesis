using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;
using SkillIssue.CharacterSpace;
using SkillIssue.StateMachineSpace;

public class AttackClass : MonoBehaviour, IHitboxResponder
{
    private AttackData m_data;
    public Hitbox[] hitboxes;
    public Character character;
    private AttackData currentAttack;
    private bool hit = false;
    public int repeatedAttack = 0;
    public int sameLimit;
    [SerializeField]
    public Coroutine landCheck = null;
    public int waitFrame = 0;
    public int landFrame = 0;
    public void Attack(AttackData data)
    {
        Debug.Log("Using " + data + character.currentAction);
        if (character.stateMachine.currentState == character.stateMachine.jumpState)
        {
            Debug.Log("StartedCoroutine");
            landCheck = StartCoroutine(CheckForLandCancel(data));
        }
      //check if can cancel
        if (character.stateMachine.currentAction != ActionStates.None)
        {   
            if(!Cancelable(data))
            {
                Debug.Log("Can't cancel");
                character.storedAttack = data;
                return;
            }
            Debug.Log("cancelled: " + m_data.name + data.name);
        }
        if (data.animation != null)
        {
            character.characterAnimation.AddAnimation(AnimType.Attack, data.animation.name);
        }
        repeatedAttack = 0;
        character.stateMachine.currentAction = ActionStates.Attack;
        hit = false;
        m_data = data;
        currentAttack = null;      
        foreach (Hitbox hitbox in hitboxes)
        { 
            hitbox.setResponder(this);
        }
    
        //Attack
    }

    public void CollisionedWith(Collider2D collider)
    {
        if (currentAttack != m_data)
        {
            currentAttack = m_data;
            Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
            hurtbox?.GetHitBy(m_data);
            hit = true;
            character.HitConnect(m_data);
        }
        if (character.storedAttack != null)
        {
            character.PerformAttack(character.storedAttack.attackType);         
        }

    }
    public void StartCheckingCollisions()
    { 
        foreach (Hitbox hitbox in hitboxes)
        {
            hitbox.StartCheckingCollision();
        }
    }
    public void StopCheckingCollisions()
    {
        foreach (Hitbox hitbox in hitboxes)
        {
            hitbox.StopCheckingCollision();
        }
    }
    private bool Cancelable(AttackData data)
    {
        if (character.stateMachine.currentAction == ActionStates.Landing)
        {
            return true;
        }
        if (!hit)
        {
            return false;
        }
           

        if (data.canceleableSelf && data == m_data)
        {
            if (character.currentCombo.Count >= sameLimit)
            {
                int count = character.currentCombo.Count -1;
                while (count >= character.currentCombo.Count - sameLimit)
                {
                    if (data == character.currentCombo[count])
                    {

                        Debug.Log("Same Attack");
                        repeatedAttack++;
                    }
                    count--;
                }
                if (repeatedAttack >= sameLimit)
                {
                    Debug.Log("Reached Limit");
                    return false;
                }
            }
        }
        if (data != m_data)
        {
            repeatedAttack = 0;
        }
        if (data == character.storedAttack)
        {
            character.storedAttack = null;
        }
        if(data.attackState == AttackState.Jumping)
        {
            return false;
        }
        if (!data.canceleableSelf && data == m_data)
        {
                return false;

        }
      
            foreach (AttackType canceltype in m_data.cancelableTypes)
        {
            if (data.attackType == canceltype)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator CheckForLandCancel(AttackData data)
    {
        waitFrame = 0;
        landFrame = 0;
        while (waitFrame < 10)
        {
            Debug.Log("Frame" + waitFrame + character.currentState);
             if (character.currentState == States.Standing)
            {
                while(landFrame < 5)
                {
                    Debug.Log("LandFrame" + landFrame + character.currentAction);
                    if(character.currentAction == ActionStates.Landing)
                    {
                        character.PerformAttack(data.attackType);
                    }         
                    yield return null;
                    landFrame++;
                }
            }
               
            yield return null;
            waitFrame++;
        }
        Debug.Log("Coroutine End");
    }
}
