using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;

public class AttackClass : MonoBehaviour, IHitboxResponder
{
    public AttackData data;
    public Hitbox hitbox;

    public void Attack()
    {
        hitbox.setResponder(this);
        //Attack
    }

    public void collisionedWith(Collider2D collider)
    {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        hurtbox?.GetHitBy(data);

    }
}
