using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;
using SkillIssue.CharacterSpace;

public class AttackClass : MonoBehaviour, IHitboxResponder
{
    private AttackData m_data;
    public Hitbox[] hitboxes;
    public Character character;
    private AttackData currentAttack;

    public void Attack(AttackData data)
    {
        m_data = data;
        currentAttack = null;
        foreach (Hitbox hitbox in hitboxes)
        { 
            hitbox.setResponder(this);
        }
        character.animator.Play(data.animation.name);
        //Attack
    }

    public void CollisionedWith(Collider2D collider)
    {
        if (currentAttack != m_data)
        {
            currentAttack = m_data;
            Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
            hurtbox?.GetHitBy(m_data);
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

}
