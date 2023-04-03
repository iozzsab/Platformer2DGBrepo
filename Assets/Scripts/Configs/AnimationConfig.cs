using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum AnimState
    {
        Idle = 0,
        Run = 1,
        RunAttack = 2,
        Walk = 3,
        Jump = 4,
        Dead = 5,
        Hurt = 6,
        Defend = 7,
        Protect = 8,
        Attack1 = 9,
        Attack2 = 10,
        Attack3 = 11
        
    }

    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs / AnimationConfig", order = 1)]
    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public AnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}