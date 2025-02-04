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
            public const string isDead = "isDead";
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
            _unitStateMachine.StateChanged += StateMachine_StateChanged;
        }

        void OnDisable()
        {
            _unitStateMachine.StateChanged -= StateMachine_StateChanged;
        }

        private void StateMachine_StateChanged(IState state)
        {
            switch (state.ID)
            {
                case 1:
                    _animator.SetBool(AnimatorParameters.isWalk, false);
                    break;
                case 2:
                    _animator.SetBool(AnimatorParameters.isWalk, true);
                    break;
                case 3:
                    _animator.SetBool(AnimatorParameters.isWalk, false);
                    _animator.SetBool(AnimatorParameters.isDead, true);
                    break;
                default:
                    _animator.SetBool(AnimatorParameters.isWalk, false);
                    break;
            }
        }
    }
}
