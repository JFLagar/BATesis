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
        public bool blockCheck = false;

        public void Update()
        {
            if (!blockCheck)
                return;
            if (character.x == character.wallx)
            {
                state = ColliderState.Open;
            }
            else
            {
                state = ColliderState.Closed;
            }
        }
        public void GetHitBy(AttackData data)
        {
            if (state == ColliderState.Closed)
            {
                return;
            }
            else
            {

                if (!blockCheck)
                {
                    character.GetHit(data);
                    Debug.Log("Got hit by:" + data.name + "/" + name);
                }     
                else
                {
                    character.GetHit(data, true);
                    Debug.Log("Blocking Zone");
                }
                
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

    }
}
