using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;
using SkillIssue.CharacterSpace;

public class Projectile : MonoBehaviour , IHitboxResponder
{
    public Character parent;
    public int hitPoints;
    public SpriteRenderer m_renderer;
    public AttackData data;
    public Vector2 origin;
    public Vector2 trajectory;
    public Hitbox hitbox;
    public Hurtbox m_hurtbox;
    public float speed;
    public Animator animator;
    private AttackData currentAttack;
    public Transform collisions;
    // Start is called before the first frame update
    void Start()
    {
        if (trajectory.x == -1)
        {
            m_renderer.flipX = true;
            collisions.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            m_renderer.flipX = false;
            collisions.eulerAngles = new Vector3(0, 0, 0);
        }
        hitbox.setResponder(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(trajectory * speed * Time.deltaTime);
        if (!m_renderer.isVisible)
        {
            trajectory = Vector2.zero;
            Destroy(this.gameObject,0.5f);
        }
    }
    public void CollisionedWith(Collider2D collider)
    {
        if(currentAttack != data)
        {
            Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
            hurtbox?.GetHitBy(data);
            if (hurtbox.blockCheck)
                return;
            trajectory = Vector2.zero;
            animator.SetTrigger("Collided");
            parent.HitConnect(data);
            Destroy(this.gameObject, 0.5f);
            currentAttack = data;
        }
      
    }
}
