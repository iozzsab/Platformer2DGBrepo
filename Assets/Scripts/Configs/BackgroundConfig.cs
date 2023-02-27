using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum BackgroundAnimState
    {
        BackgroundDefault = 0,
        BackgroundSnow = 1,
    }

    [CreateAssetMenu(fileName = "BackgroundAnimCfg", menuName = "Configs / BackgroundConfig", order = 2)]
    public class BackgroundConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public BackgroundAnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Backgrounds = new List<SpriteSequence>();
    }
}