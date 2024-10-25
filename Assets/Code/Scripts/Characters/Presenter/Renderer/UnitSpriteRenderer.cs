using UnityEngine;

namespace IntoTheWilds
{
    public enum FaceDirections
    {
        ToRight = 0,
        ToLeft = 1
    }

    public class UnitSpriteRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        private FaceDirections _currentFaceDirections;
        public FaceDirections FaceDirections
        {
            get
            {
                return _currentFaceDirections;
            }
            set
            {
                _currentFaceDirections = value;
                FlipSprite();
            }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckAndFlipSprite();
        }

        private void CheckAndFlipSprite()
        {
            FaceDirections newFaceDirection = GetNewFaceDirection();

            if (FaceDirections != newFaceDirection)
            {
                FaceDirections = newFaceDirection;
            }
        }

        private FaceDirections GetNewFaceDirection()
        {
            float velocityX = _rigidbody2D.linearVelocityX;

            if (velocityX > 0.2)
            {
                return FaceDirections.ToRight;
            }
            else if (velocityX < -0.2)
            {
                return FaceDirections.ToLeft;
            }

            return FaceDirections;
        }

        private void FlipSprite()
        {
            if (_currentFaceDirections == FaceDirections.ToRight)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}
