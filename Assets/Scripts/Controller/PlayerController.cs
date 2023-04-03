using Unity.VisualScripting;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private AnimationConfig _config;
        private SpriteAnimatorController _playerAnimator;
        private LevelObjectView _playerView;
        private ContactPooler _contactPooler;

        private float _walkSpeed = 150f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f;
        private float _jumpForce = 15f;
        private float _jupmTreshold = 1f;
        
        private bool _isJumping;
        private bool _isMoving;
        
        private bool _isAlive = true;
        private int _health = 100;
        
        private Transform _playerTransform;
        private Rigidbody2D _playerRigidbody;



        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private float _yVelocity = 0;
        private float _xVelocity = 0;
        private float _xAxisInput;


        public PlayerController(InteractiveObjectView playerView)
        {
            _playerView = playerView;
            _playerTransform = playerView._transform;
            _playerRigidbody = playerView._rb;

            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, _animationSpeed);
            _contactPooler = new ContactPooler(_playerView._collider);

            playerView.TakeDamage += TakeBullet;
        }

        public void TakeBullet(BulletView bullet)

        {
            _health -= bullet.DamagePoint;
            //_playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Hurt, true, _animationSpeed);
        }


        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _playerRigidbody.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }


        public void Update()
        {
            if (_health <= 0)
            {
                _health = 0;
                _isAlive = false;
            }
            else
                _isAlive = true;

            if (_isAlive)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle,
                    true, _animationSpeed);
            }
            else
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Dead, false, _animationSpeed);
            }


            _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJumping = Input.GetAxis("Vertical") > 0;
            _yVelocity = _playerRigidbody.velocity.y;


            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;


            if (_isMoving && _isAlive)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _playerRigidbody.velocity = new Vector2(_xVelocity, _playerRigidbody.velocity.y);
            }


            if (_contactPooler.IsGrounded && _isAlive)
            {
                if (_isJumping && _yVelocity <= _jupmTreshold)
                {
                    _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jupmTreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }
        }
    }
}