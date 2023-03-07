using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {

        private PlayerController _playerController;
        [SerializeField] private LevelObjectView _playerView;

        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
        }

        private void Update()
        {
            _playerController.Update();
        }
    }
}