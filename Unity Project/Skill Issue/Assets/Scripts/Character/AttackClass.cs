using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;

public class AttackClass : MonoBehaviour, IHitboxResponder
{
    public AttackData data;
    public Hitbox[] hitboxes;

    public void Attack()
    {
        foreach (Hitbox hitbox in hitboxes)
        { 
            //turn the collision to closed for inactive hitboxes
            hitbox.setResponder(this);
        }       
        //Attack
    }

    public void CollisionedWith(Collider2D collider)
    {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        hurtbox?.GetHitBy(data);

    }
}
