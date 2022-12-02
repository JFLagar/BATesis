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

    public void Attack(AttackData data)
    {
      //check if can cancel
        if (character.stateMachine.currentAction != ActionStates.None)
        {
            if(!Cancelable(data))
            return;      
        }
        hit = false;
        m_data = data;
        currentAttack = null;
        if (data.animation != null)
        {
            character.animator.Play(data.animation.name, 0, 0f);
        }
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
            character.comboHit++;
            hit = true;
        }
        if (character.storedAttack != null)
        {
            Attack(character.storedAttack);
            Debug.Log("Late cancel into:" + character.storedAttack);

           
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
        if (!hit)
            return false;
        if (data == character.storedAttack)
        {
            Debug.Log("Trying stored Attack");
            character.storedAttack = null;
        }
        if(character.oponent.currentAction == ActionStates.None || character.oponent.currentAction == ActionStates.Attack)
        {
            character.storedAttack = data;
            Debug.Log("Can't cancelel " + m_data.name + "into " + data.name + ",whiffed");
            return false;
        }
        if(data.attackState == AttackState.Jumping)
        {
            return false;
        }
        if (!data.canceleableSelf && data == m_data)
        {
                Debug.Log("Can't cancel" + m_data.name + "into " + data.name + ", can't cancel into self");
                return false;

        }
            foreach (AttackType canceltype in m_data.cancelableTypes)
        {
            if (data.attackType == canceltype)
            {
                Debug.Log("Canceled " + m_data.name + "into " + data.name);
                return true;
            }
        }
        Debug.Log("Can't cancel " + m_data.name + "into " + data.name + ", not proper cancel type");
        return false;
    }

}
