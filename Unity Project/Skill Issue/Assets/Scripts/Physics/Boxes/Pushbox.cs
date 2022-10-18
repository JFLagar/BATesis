using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue;

public enum PushType
{
    Character,
    Ground,
    Wall
}
public class Pushbox : MonoBehaviour
{
    public Collider2D m_collider;
    public LayerMask mask;
    public bool useSphere = false;
    public Vector3 hitboxSize;
    public Color color;
    public ColliderState state;

    private IHitboxResponder responder = null;
    public SkillIssue.CharacterSpace.Character character = null;
    public float push = 60;
    void FixedUpdate()
    {
        CheckCollision();
        if (state == ColliderState.Colliding)
        {
            character.CharacterPush(0);
        }
    }
    void CheckCollision()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, hitboxSize, 0, (mask));
        if(colliders.Length <= 1)
        {
            state = ColliderState.Open;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != m_collider)
            {
                Collider2D aCollider = colliders[i];
                responder?.CollisionedWith(aCollider);
            }
        }
    
    }
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(hitboxSize.x * 2, hitboxSize.y * 2, hitboxSize.z * 2)); // Because size is halfExtents
    }
    public void setResponder(IHitboxResponder hitboxResponder)
    {
        responder = hitboxResponder;
    }
    public void HandleCollision(Pushbox pushbox)
    {
   
        
            state = ColliderState.Colliding;
        if (character.wall)
        {
            pushbox.character.wall = true;
            pushbox.character.wallx = character.wallx;
            if (pushbox.character.applyGravity)
            {
                pushbox.character.CharacterPush(character.wallx);
            }
        }
        if (character.x != 0 && character.x == character.faceDir)
        {
            if (character.applyGravity)
            {
                pushbox.character.CharacterPush(character.faceDir/2 * push * Time.deltaTime);
            }
            else
            {
                pushbox.character.CharacterPush(character.x/2 * push * Time.deltaTime);
            }
        }
        else
        {
            pushbox.character.CharacterPush(0);
        }

    }
}
