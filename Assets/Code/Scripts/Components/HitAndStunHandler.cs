using System.Collections;
using Tools.StateMachine;
using UnityEngine;
using VContainer;

namespace IntoTheWilds
{
    public interface IStunble
    {
        void Stun(bool isStunned);
    }

    public class HitAndStunHandler : MonoBehaviour
    {
        [SerializeField] private bool _isStunnedAfterHit = true;
        [SerializeField] private float _maxAcceleration = 5f;
        [SerializeField] private float _stunDuration = 0.25f;
        private Vector2 _desiredVelocity;

        [SerializeField] private bool _isFlashingAfterHit = true;
        [SerializeField] private float _flashDuration = 0.15f;
        [SerializeField] private Color _flashColor = Color.white;

        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private bool _isStunned = false;

        private Rigidbody2D _unitRigidbody2D;
        private IStunble _stunble;
        private StateMachine _unitStateMachine;

        [Inject]
        public void Constuct(Rigidbody2D rigidbody2D, IStunble stunble, StateMachine unitStateMachine)
        {
            _unitRigidbody2D = rigidbody2D;
            _stunble = stunble;
            _unitStateMachine = unitStateMachine;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
        }

        private void FixedUpdate()
        {
            if (_isStunned && _isStunnedAfterHit)
            {
                var velocity = _unitRigidbody2D.linearVelocity;
                float _maxSpeedChange = _maxAcceleration * Time.fixedDeltaTime;

                velocity = Vector2.MoveTowards(velocity, _desiredVelocity, _maxSpeedChange);

                _unitRigidbody2D.linearVelocity = velocity;
            }
        }

        public void ApplyHitAndStun(Vector2 sourcePosition)
        {
            if (_isStunned == false)
            {
                _ = StartCoroutine(StunCoroutine(sourcePosition));
                return;
            }

            StunEffects(sourcePosition);
        }

        private IEnumerator StunCoroutine(Vector2 sourcePosition)
        {
            _stunble.Stun(true);

            yield return new WaitForSeconds(0.1f);

            _isStunned = true;

            if (_isStunnedAfterHit)
            {
                _unitStateMachine.SetEnable(false);
            }

            StunEffects(sourcePosition);

            yield return new WaitForSeconds(_stunDuration);

            _stunble.Stun(false);

            if (_isStunnedAfterHit)
            {
                _unitStateMachine.SetEnable(true);
            }               

            _isStunned = false;
        }

        private void StunEffects(Vector2 sourcePosition)
        {
            _desiredVelocity = (_unitRigidbody2D.position - sourcePosition).normalized;

            if (_isFlashingAfterHit == true)
            {
                _ = StartCoroutine(StunFlashEffect());
            }
        }

        private IEnumerator StunFlashEffect()
        {
            _material.SetColor("_FlashColor", _flashColor);

            float currentFlashAnount;
            float elapsetTime = 0f;

            while (elapsetTime < _flashDuration)
            {
                elapsetTime += Time.deltaTime;
                currentFlashAnount = Mathf.Lerp(1.5f, 0f, elapsetTime / _flashDuration);

                _material.SetFloat("_FlashAmount", currentFlashAnount);

                yield return null;
            }
        }
    }
}