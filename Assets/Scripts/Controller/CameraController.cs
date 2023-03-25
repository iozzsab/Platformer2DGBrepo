using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace PlatformerMVC
{
    public class CameraController
    {
        private LevelObjectView _player;
        private Transform _playerT;
        private Transform _cameraT;

        private float _cameraSpeed = 1.2f;

        private float X;
        private float Y;

        private float offsetY;
        private float offsetX;

        private float _xAxisInput;
        private float _yAxisInput;
        
        private float _treshold;

        public CameraController(LevelObjectView player, Transform cameraT)
        {
            _player = player;
            _playerT = player.transform;
            _cameraT = cameraT;
            _treshold = 0.2f;
        }

        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = _player._rb.velocity.y;

            X = _playerT.position.x;
            Y = _playerT.position.y;

            if (_xAxisInput > _treshold)
            {
                offsetX = 4;
                
            }
            else if (_xAxisInput < -_treshold)
            {
                offsetX = -4;
            }
            else
            {
                offsetX = 0;
            }

            if (_yAxisInput > _treshold)
            {
                offsetY = 4;
            }
            else if (_yAxisInput < -_treshold)
            {
                offsetY = -4;
            }
            else
            {
                offsetY = 0;
            }
            
            _cameraT.position = Vector3.MoveTowards(_cameraT.position,
                new Vector3(X+offsetX, Y+offsetY, _cameraT.position.z), 
                _cameraSpeed * Time.deltaTime);
        }
            
    }
}