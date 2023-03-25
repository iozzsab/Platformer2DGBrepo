using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private GeneratorLevelView _generatorView;
        
        private PlayerController _playerController;
        private CannonController _cannonController;
        private EmitterController _emitterController;
        private GeneratorController _generatorController;
        private CameraController _cameraController;
        


        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cannonController = new CannonController(_cannonView._muzzleT, _playerView._transform);
            _emitterController = new EmitterController(_cannonView._bullets, _cannonView._emitterT);
            _generatorController = new GeneratorController(_generatorView);
            _cameraController = new CameraController(_playerView, Camera.main.transform);
            _generatorController.Start();
        }

        private void Update()
        {
            _playerController.Update();
            _cannonController.Update();
            _emitterController.Update();
            _cameraController.Update();
        }
    }
}