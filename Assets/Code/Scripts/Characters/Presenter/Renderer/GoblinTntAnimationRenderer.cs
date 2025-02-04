using Tools.BehaviorTree;
using Tools.StateMachine;
using UnityEngine;
using VContainer;

namespace IntoTheWilds
{
    public class GoblinTntAnimationRenderer : MonoBehaviour
    {
        private StateMachine _unitStateMachine;
        private Animator _animator;
        private UnitSpriteRenderer _spriteRenderer;
        private BehaviorTree _aiTree;

        [SerializeField] private Transform _leftSpawnPointDynamite;
        [SerializeField] private Transform _rightSpawnPointDynamite;
        [SerializeField] private GameObject _dynamitePrefab;

        private static class AnimatorParameters
        {
            public const string State = "state";
        }

        [Inject]
        public void Constuct(StateMachine unitStateMachine, BehaviorTree unitAiTree)
        {
            _unitStateMachine = unitStateMachine;
            _aiTree = unitAiTree;

        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<UnitSpriteRenderer>();
        }

        void OnEnable()
        {
            _unitStateMachine.StateChanged += StateMachine_StateChanged;
        }

        void OnDisable()
        {
            _unitStateMachine.StateChanged -= StateMachine_StateChanged;
        }

        public void StateMachine_StateChanged(IState state)
        {
            _animator.SetInteger(AnimatorParameters.State, state.ID);
        }

        public void Animator_TimeToSpawnDynamite()
        {
            object target = _aiTree.RootNode.GetData("target");

            Transform targetTransform = (Transform)target;

            if (_spriteRenderer.FaceDirections == FaceDirections.ToRight)
            {
                SpawnDynamite(_rightSpawnPointDynamite.position, targetTransform.position);
            }
            else if (_spriteRenderer.FaceDirections == FaceDirections.ToLeft)
            {
                SpawnDynamite(_leftSpawnPointDynamite.position, targetTransform.position);
            }
        }

        private void SpawnDynamite(Vector3 startPosition, Vector3 targetPosition)
        {
            GameObject dynamite = Instantiate(_dynamitePrefab, startPosition, Quaternion.identity);
            
            Rigidbody2D rb = dynamite.GetComponent<Rigidbody2D>();

            DynamiteMovement movement = dynamite.GetComponent<DynamiteMovement>();
            movement.Initialize(5f);

            Vector3 direction = targetPosition - startPosition;
            float distance = direction.magnitude;

            float requiredSpeed = Mathf.Sqrt(2 * movement.Deceleration * distance);
            rb.linearVelocity = direction.normalized * requiredSpeed;
        }
    }
}
