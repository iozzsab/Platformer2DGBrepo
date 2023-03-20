using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlatformerMVC
{
    public class EmitterController
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _tr;

        private int _index;

        private float _timeTillNewBull;
        private float _startSpeed = 15;
        private float _delay = 1;

        public EmitterController(List<BulletView> bulletViews, Transform emitterTr)
        {
            _tr = emitterTr;
            foreach (BulletView bulletView in bulletViews)
            {
                _bulletControllers.Add(new BulletController(bulletView));
            }
        }

        public void Update()
        {
            if (_timeTillNewBull > 0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNewBull -= Time.deltaTime;
            }
            else
            {
                _timeTillNewBull = _delay;
                _bulletControllers[_index].Trow(_tr.position, - _tr.up * _startSpeed);
                _index++;
                if (_index >= _bulletControllers.Count) _index = 0;
            }
        }
    }
}