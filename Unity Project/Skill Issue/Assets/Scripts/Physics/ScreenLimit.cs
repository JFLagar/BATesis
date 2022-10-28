using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;

public class ScreenLimit : MonoBehaviour
{
    public bool isWall;
    public int x;
    public Collider2D m_collider;
    public LayerMask mask;
    public Vector3 hitboxSize;
    public Color color;
    public bool cameraWall = false;
    private Pushbox previousCollision;


    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(m_collider.bounds.center, m_collider.bounds.size, 0, (mask));

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D aCollider = colliders[i];
            Pushbox collidedbox = colliders[i].GetComponent<Pushbox>();
            if (!cameraWall)
            {
                if (!isWall)
                { collidedbox?.character.IsGrounded(true); }
                else
                {
                    collidedbox?.character.SetWall(true, x);
                }
            }
            else
            {
                previousCollision = collidedbox;
                collidedbox?.character.SetWall(true, x);
            }


        }
        if (colliders.Length == 0)
        {
            if (previousCollision == null)
                return;
            previousCollision.character.SetWall(false, x);
            previousCollision = null;
        }
      
    }
    void OnDrawGizmoSelected()
    {
        Gizmos.color = color;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(hitboxSize.x * 2, hitboxSize.y * 2, hitboxSize.z * 2)); // Because size is halfExtents
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pushbox pushbox = collision.gameObject.GetComponent<Pushbox>();
        pushbox?.character.SetWall(true, x);
        Debug.Log("Entered" + collision.name);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Pushbox pushbox = collision.gameObject.GetComponent<Pushbox>();
        pushbox?.character.SetWall(false, x);
        Debug.Log("Exit" + collision.name);
    }

}
