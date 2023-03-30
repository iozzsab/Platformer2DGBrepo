using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        public Color _completedColor;
        public Color _defaulColor;

        private void Awake()
        {
            _defaulColor = _spriteRenderer.color;
        }

        public void ProcessComplete()
        {
            _spriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            _spriteRenderer.color = _defaulColor;
        }
    }
}