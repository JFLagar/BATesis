using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillIssue
{
    public class Hurtbox : MonoBehaviour
    {
        public Collider2D hurtboxCollider;
        private ColliderState state = ColliderState.Open;

        public Vector3 hitboxSize = Vector3.one;
        public float radius = 0.5f;
        public Color inactiveColor;
        public Color collisionOpenColor;
        public Color collidingColor;

        public void GetHitBy(AttackData data)
        {
            // Do something with the damage and the state
        }

        void OnDrawGizmos()
        {
            CheckGizmoColor();
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, new Vector3(hitboxSize.x * 2, hitboxSize.y * 2, hitboxSize.z * 2)); // Because size is halfExtents
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
