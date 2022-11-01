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

    public void Attack(AttackData data, AttackData previousattack = null)
    {
      
        if (character.stateMachine.currentAction != ActionStates.None)
        {
            return;      
        }
        m_data = data;
        Debug.Log("Cancelable");
        hit = false;
        currentAttack = null;
        foreach (Hitbox hitbox in hitboxes)
        { 
            hitbox.setResponder(this);
        }
        if (data.animation != null)
            character.animator.Play(data.animation.name);
        else
            Debug.Log(data.name);
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
    private bool Cancelable(AttackType type)
    {

        foreach (AttackType canceltype in m_data.cancelableTypes)
        {
            if (type == canceltype)
            {
                return true;
            }
        }
        return false;
    }

}
