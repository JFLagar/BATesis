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
        public Vector3 hitboxSize = Vector3.one;
        public float radius = 0.5f;
        public Color inactiveColor;
        public Color collisionOpenColor;
        public Color collidingColor;

        public ColliderState state;
        private IHitboxResponder responder = null;

        void Update()
        {
            CheckCollision();
        }
        void CheckCollision()
        {
            if (state == ColliderState.Closed) { return; }
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, hitboxSize, 0, mask);

            for (int i = 0; i < colliders.Length; i++)
            {
                Collider2D aCollider = colliders[i];
                responder?.CollisionedWith(aCollider);
            }

            state = colliders.Length > 0 ? ColliderState.Colliding : ColliderState.Open;

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
