using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillIssue.CharacterSpace;

namespace SkillIssue
{
    public class Hurtbox : MonoBehaviour
    {
        public Character character;
        public Collider2D hurtboxCollider;
        public ColliderState state = ColliderState.Open;

        public Vector3 hitboxSize;
        public Color inactiveColor;
        public Color collisionOpenColor;
        public Color collidingColor;

        public void GetHitBy(AttackData data)
        {
            if (state == ColliderState.Closed)
            {
                Debug.Log("Got hit by but closed");
            }
            else
            Debug.Log("Got hit by:" + data.name);
            character.GetHit(data);
        }

        void OnDrawGizmosSelected()
        {
            CheckGizmoColor();
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(hitboxSize.x * 2, hitboxSize.y * 2, hitboxSize.z * 2)); // Because size is halfExtents
        }
        void CheckGizmoColor()
        {
            switch (state)
            {
                case ColliderState.Closed:
                    Gizmos.color = inactiveColor;
                    break;
                case ColliderState.Open:
                    Gizmos.color = collisionOpenColor;
                    break;
                case ColliderState.Colliding:
                    Gizmos.color = collidingColor;
                    break;
            }
        }

    }
}
