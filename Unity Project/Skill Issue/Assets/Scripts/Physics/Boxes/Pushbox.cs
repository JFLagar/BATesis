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
    public Vector3 hitboxSize = Vector3.one;
    public float radius = 0.5f;
    public Color color;

    private IHitboxResponder responder = null;
    public SkillIssue.CharacterSpace.Character character = null;
    public PushType type;
    public float wall;
    public float push = 60;
    void Update()
    {
        CheckCollision();
    }
    void CheckCollision()
    {
        if (type != PushType.Character)
            return;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, hitboxSize, 0, (mask));

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
        switch (type)
        {
            case PushType.Character:
                Debug.Log("Collided with:" + pushbox.character.gameObject.name.ToString());
                if (character.wall)
                {
                    pushbox.character.wall = true;
                    pushbox.character.wallx = character.wallx;
                    if(pushbox.character.applyGravity)
                    {
                        pushbox.character.CharacterPush(character.wallx);
                    }
                }
                if (pushbox.character.applyGravity)
                    character.CharacterPush(pushbox.character.faceDir * push * Time.deltaTime);
                else
                character.CharacterPush(pushbox.character.x * push * Time.deltaTime);
                break;
            case PushType.Ground:
                pushbox.character.isGrounded = true;
                break;
            case PushType.Wall:
                pushbox.character.wall = true;
                pushbox.character.wallx = wall;
                break;
        }
    }
}
