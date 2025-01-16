using Tools.StateMachine;
using UnityEngine;
using VContainer;

namespace IntoTheWilds
{
    public class GoblinTorchAnimatorRenderer : MonoBehaviour
    {
        private StateMachine _unitStateMachine;
        private Animator _animator;
        private UnitSpriteRenderer _spriteRenderer;

        [SerializeField] public GameObject _leftAttackTrigger;
        [SerializeField] public GameObject _rightAttackTrigger;

        private static class AnimatorParameters
        {
            public const string State = "state";
            public const string StateChanged = "stateChanged";
        }

        [Inject]
        public void Constuct(StateMachine unitStateMachine)
        {
            _unitStateMachine = unitStateMachine;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<UnitSpriteRenderer>();
        }

        void OnEnable()
        {
            _unitStateMachine.StateChanged += OnStateChanged;
        }

        void OnDisable()
        {
            _unitStateMachine.StateChanged -= OnStateChanged;
        }


        public void OnStateChanged(IState state)
        {
            _animator.SetInteger(AnimatorParameters.State, state.ID);
            _animator.SetTrigger(AnimatorParameters.StateChanged);
        }

        public void TimeToSpawnTriggerForAttack()
        {
            if (_spriteRenderer.FaceDirections == FaceDirections.ToRight)
            {
                _rightAttackTrigger.SetActive(true);
            }
            else if (_spriteRenderer.FaceDirections == FaceDirections.ToLeft)
            {
                _leftAttackTrigger.SetActive(true);
            }
        }
    }
}
