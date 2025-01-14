using Tools.StateMachine;
using UnityEngine;
using VContainer;

namespace IntoTheWilds
{
    [RequireComponent(typeof(UnitSpriteRenderer))]
    public class SheepAnimatorRenderer : MonoBehaviour
    {
        private StateMachine _unitStateMachine;
        private Animator _animator;

        private static class AnimatorParameters
        {
            public const string isWalk = "walk";
        }

        [Inject]
        public void Constuct(StateMachine unitStateMachine)
        {
            _unitStateMachine = unitStateMachine;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            _unitStateMachine.StateChanged += OnStateChanged;
        }

        void OnDisable()
        {
            _unitStateMachine.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(IState state)
        {
            switch (state.ID)
            {
                case 1:
                    _animator.SetBool(AnimatorParameters.isWalk, false);
                    break;
                case 2:
                    _animator.SetBool(AnimatorParameters.isWalk, true);
                    break;
                default:
                    _animator.SetBool(AnimatorParameters.isWalk, false);
                    break;
            }
        }
    }
}
