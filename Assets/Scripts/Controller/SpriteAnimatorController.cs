using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

namespace PlatformerMVC

{
    public class SpriteAnimatorController : IDisposable
    {
        private sealed class Animation
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop;
            public float Speed = 10;
            public float Counter = 0;
            public bool Sleep;


            public void Update()
            {
                if (Sleep) return;
                Counter += Time.deltaTime * Speed;
                if (Loop)
                {
                    while (Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }

        private AnimationConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimation = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimatorController(AnimationConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if(_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleep = false;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }

            }
            else
            {
                _activeAnimation.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
                
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimation.ContainsKey(sprite))
            {
                _activeAnimation.Remove(sprite);
            }
        }
        

        public void Update()
        {
            foreach (var animation in _activeAnimation)
            {
                animation.Value.Update();
                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
                
            }
            
        }


        public void Dispose()
        {
        }
    }
}