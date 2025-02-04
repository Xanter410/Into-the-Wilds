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

        [SerializeField] private GameObject _leftAttackTrigger;
        [SerializeField] private GameObject _rightAttackTrigger;

        private static class AnimatorParameters
        {
            public const string State = "state";
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

        public void Animator_SpawnTriggerForAttack()
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
