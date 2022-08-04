using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;
public class FloorScript : MonoBehaviour
{
    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
       collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0, LayerMask.NameToLayer("Pushbox"));
        if (colliders.Length != 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Character character = colliders[i].GetComponent<Character>();
                character?.IsGrounded(true);
                Debug.LogWarning("Collided");
            }
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        character?.IsGrounded(true);
        Debug.Log("Colliding");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        character?.IsGrounded(false);
        Debug.Log("Exit");
    }
}
