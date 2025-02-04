using System;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinTntAI : BehaviorTree, ITickable, IStartable, IMove, IAttack, IStunble
    {
        public event Action AttackPressed;

        private readonly Transform _transform;

        private Vector2 _spawnPoint;
        private bool _isStunned = false;

        private Vector2 _moveInput = Vector2.zero;

        public GoblinTntAI(Rigidbody2D rigidbody2D)
        {
            _transform = rigidbody2D.transform;
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new AI_FindPlayerInRange(_transform, 3f),

                    new Selector(new List<Node>
                    {
                        new AI_CheckTargetInRange(_transform, 4f),
                        new AI_ClearTarget(NodeState.FAILURE)
                    }),

                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new AI_CheckTargetInRange(_transform, 2.5f),
                            new AI_Attack(this, 4f)
                        }),

                        new Sequence(new List<Node>
                        {
                            new AI_CheckDistanceToSpawnPoint(_transform, _spawnPoint, 10f, 5f),
                            new AI_MoveToTarget(this, _transform, 2.7f)
                        })
                    })

                }),

                new AI_IdleWalkNearSpawn(this, _transform, _spawnPoint, 3f, 3f)
            });

            return root;
        }

        void IAttack.OnAttackPressed()
        {
            ((IMove)this).SetMoveInput(Vector2.zero);

            AttackPressed?.Invoke();
        }

        Vector2 IMove.RetrieveMoveInput()
        {
            return _moveInput;
        }

        void IMove.SetMoveInput(Vector2 moveDirection)
        {
            _moveInput = moveDirection;
        }

        void IStartable.Start()
        {
            _spawnPoint = _transform.position;

            Setup();
        }

        void IStunble.Stun(bool isStunned)
        {
            if (isStunned == true)
            {
                _isStunned = true;
                ((IMove)this).SetMoveInput(Vector2.zero);
            }
            else
            {
                _isStunned = false;
            }
        }

        void ITickable.Tick()
        {
            if (_isStunned != true)
            {
                Update();
            }
        }
    }
}
