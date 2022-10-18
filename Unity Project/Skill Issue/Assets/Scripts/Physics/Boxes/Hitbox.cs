using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkillIssue
{
    public enum ColliderState
    {
        Closed,
        Open,
        Colliding
    }
    public class Hitbox : MonoBehaviour
    {
        public LayerMask mask;
        public bool useSphere = false;
        public Vector3 hitboxSize;
        public Color inactiveColor;
        public Color collisionOpenColor;
        public Color collidingColor;

        public ColliderState state;
        private IHitboxResponder responder = null;

        void FixedUpdate()
        {
            CheckCollision();
        }
        void CheckCollision()
        {
            if (state == ColliderState.Closed) { return; }

            Collider2D collider = Physics2D.OverlapBox(transform.position, hitboxSize, 0, mask);
            if (collider)
            {
                state = ColliderState.Colliding;

                responder?.CollisionedWith(collider);
            }
            else
            {
                state = ColliderState.Open;
            }

            

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
        public void StartCheckingCollision()
        {
            state = ColliderState.Open;
        }

        public void StopCheckingCollision()
        {
            state = ColliderState.Closed;
        }
        public void setResponder(IHitboxResponder hitboxResponder)
        {
            responder = hitboxResponder;
        }
    }
}
