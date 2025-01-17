using Tools.BehaviorTree;
using UnityEngine;
using VContainer.Unity;
using System.Collections.Generic;

namespace IntoTheWilds
{
    public class SheepAI : BehaviorTree, ITickable, IStartable, IMove, IStunble
    {
        private readonly Transform _transform;

        private Vector2 _moveInput = Vector2.zero;
        private bool _isStunned = false;

        public SheepAI(Rigidbody2D unitRigidbody2D) 
        {
            _transform = unitRigidbody2D.transform;        
        }

        public Vector2 RetrieveMoveInput()
        {
            return _moveInput;
        }

        public void SetMoveInput(Vector2 moveDirection)
        {
            _moveInput = moveDirection;
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new AI_FindPlayerInRange(_transform, 2f),
                    new SheepAI_EscapeTask(this,_transform)
                }),
                new SheepAI_IdleTask(this, _transform)
            });

            return root;
        }

        void IStartable.Start()
        {
            Setup();
        }

        void ITickable.Tick()
        {
            if (_isStunned != true)
            {
                Update();
            }
        }

        void IStunble.Stun(bool isStunned)
        {
            if (isStunned == true)
            {
                _isStunned = true;
                SetMoveInput(Vector2.zero);
            }
            else
            {
                _isStunned = false;
            }
        }
    }
}
