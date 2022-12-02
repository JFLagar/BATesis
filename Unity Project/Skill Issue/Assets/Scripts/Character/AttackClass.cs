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

    public void Attack(AttackData data)
    {
      //check if can cancel
        if (character.stateMachine.currentAction != ActionStates.None)
        {
            if(!Cancelable(data))
            return;      
        }
        if (repeatedAttack >= 3)
            repeatedAttack = 0;
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
            character.currentCombo.Add(m_data);
            hit = true;
        }
        if (character.storedAttack != null)
        {
            Attack(character.storedAttack);         
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
        if (character.currentCombo.Count >= 3)
        {
            for (int i = character.currentCombo.Count - 1; i == character.currentCombo.Count - 3; i--)
            {
                if (data != character.currentCombo[i])
                    break;
            }
            Debug.Log("Reached Capacity");
            return false;
        }


        if (data == character.storedAttack)
        {
            character.storedAttack = null;
        }
        if(character.oponent.currentAction == ActionStates.None || character.oponent.currentAction == ActionStates.Attack)
        {
            character.storedAttack = data;
            return false;
        }
        if(data.attackState == AttackState.Jumping)
        {
            return false;
        }
        if (!data.canceleableSelf && data == m_data)
        {
                return false;

        }
        if (data.canceleableSelf && data == m_data)
        {
            repeatedAttack++;
        }
        if (data != m_data)
        {
            repeatedAttack = 0;
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

}
