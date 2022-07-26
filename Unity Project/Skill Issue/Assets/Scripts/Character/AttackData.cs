using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SkillIssue
{
    public enum AttackType
    {
        Light,
        Heavy,
        Special
    }
    public enum AttackAttribute
    {
        Mid,
        Low,
        High
    }
    [CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/Attacks", order = 1)]
    public class AttackData : ScriptableObject
    {
        [Space]
        public AttackAttribute attackAttribute;
        public AttackType attackType;
        [Space]
        public int damage;
        public int hitstun;
        public int blockstun;
        public int proratio;
        [Space]
        public bool launcher;
        public bool dashCancel;
        public bool jumpCancel;
        public bool canceleableSelf;
        public AttackType[] cancelableTypes;
        [Space]
        public AnimationClip animation;
        public string message;
    }
}