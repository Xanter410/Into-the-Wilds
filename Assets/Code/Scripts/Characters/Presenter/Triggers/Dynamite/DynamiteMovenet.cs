using UnityEngine;

namespace IntoTheWilds
{
    public class DynamiteMovement : MonoBehaviour
    {
        public float Deceleration { get; private set; }
        private Rigidbody2D _rb;
        private bool _isInitialize = false;

        public void Initialize(float deceleration)
        {
            Deceleration = deceleration;
            _rb = GetComponent<Rigidbody2D>();
            _isInitialize = true;
        }

        void FixedUpdate()
        {
            if (_isInitialize == true)
            {
                if (_rb.linearVelocity.magnitude > 0.1f)
                {
                    Vector2 decelerationForce = -_rb.linearVelocity.normalized * Deceleration;
                    _rb.AddForce(decelerationForce, ForceMode2D.Force);
                }
                else
                {
                    _rb.linearVelocity = Vector2.zero;
                }
            }
        }
    }
}