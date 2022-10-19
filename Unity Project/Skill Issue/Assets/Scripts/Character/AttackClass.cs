using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;

public class AttackClass : MonoBehaviour, IHitboxResponder
{
    private AttackData m_data;
    public Hitbox[] hitboxes;
    public Animator m_animation;

    public void Attack(AttackData data)
    {
        m_data = data;
        
        
        foreach (Hitbox hitbox in hitboxes)
        { 
            hitbox.setResponder(this);
        }
        m_animation.Play("Base Layer." + data.animation.name);
        //Attack
    }

    public void CollisionedWith(Collider2D collider)
    {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        hurtbox?.GetHitBy(m_data);

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
