using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        private PlayerController _playerController;
        private CannonController _cannonController;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        
       

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cannonController = new CannonController(_cannonView._muzzleT, _playerView._transform);
        }

        private void Update()
        {
            _playerController.Update();
            _cannonController.Update();
        }
    }
}