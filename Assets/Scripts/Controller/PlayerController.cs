using System.Threading.Tasks;
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
        private bool _isHurt;

        private bool _isAlive = true;
        private int _healthMax = 100;

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
            _contactPooler = new ContactPooler(_playerView._collider);


            playerView.TakeDamage += TakeBullet;
        }

        public void TakeBullet(BulletView bullet)
        {
            _healthMax -= bullet.DamagePoint;
            EndHurt();
        }

        public async void EndHurt()
        {
            _isHurt = true;
            await Task.Delay(200);
            _isHurt = false;
        }


        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _playerRigidbody.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }


        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJumping = Input.GetAxis("Vertical") > 0;

            _yVelocity = _playerRigidbody.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            _contactPooler.Update();
            _playerAnimator.Update();

            if (_isMoving && _isAlive)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _playerRigidbody.velocity = new Vector2(_xVelocity, _playerRigidbody.velocity.y);
            }

            if (_isJumping)
            {
                if (_contactPooler.IsGrounded && _isAlive && _yVelocity <= _jupmTreshold)
                {
                    _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }

            RecountHealth();
            UpdateAnimation();
        }

        private void RecountHealth()
        {
            if (_healthMax <= 0)
            {
                _healthMax = 0;
                _isAlive = false;
                RecountBoxColliderSize();
            }
            else
            {
                _isAlive = true;
            }
        }

        public void UpdateAnimation()
        {
            if (!_isAlive)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Dead, false, _animationSpeed);
                return;
            }

            if (_isHurt)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Hurt, false, _animationSpeed);
                return;
            }

            if (_isMoving && !_isJumping)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true, _animationSpeed);
                return;
            }

            if (!_contactPooler.IsGrounded)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, false, _animationSpeed);
                return;
            }

            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, _animationSpeed);
        }

        public void RecountBoxColliderSize()
        {
            Vector2 spriteSize = _playerView._spriteRenderer.bounds.size;
            BoxCollider2D collider = _playerView._collider;
            collider.size = spriteSize;
        }
    }
}