using System;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinTntAI : BehaviorTree, ITickable, IStartable, IMove, IAttack, IStunble
    {
        private readonly Transform _transform;
        private Vector2 _spawnPoint;
        private bool _isStunned = false;

        private Vector2 _moveInput = Vector2.zero;

        private event Action _attackPressed;

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
                    new AI_FindAndCheckPlayerInRadius(_transform, 4f),

                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new AI_CheckPlayerInRange(_transform, 3f),
                            new AI_Attack(this, 5f)
                        }),

                        new Sequence(new List<Node>
                        {
                            new AI_CheckDistanceToSpawnPoint(_transform, _spawnPoint, 10f, 5f),
                            new AI_MoveToTarget(this, _transform, 2.7f)
                        })
                    })

                }),

                new AI_IdleWalkNearSpawn(this, _transform, _spawnPoint)
            });

            return root;
        }

        public void AttackPressed()
        {
            SetMoveInput(Vector2.zero);

            _attackPressed?.Invoke();
        }

        public Vector2 RetrieveMoveInput()
        {
            return _moveInput;
        }

        public void SetMoveInput(Vector2 moveDirection)
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
                SetMoveInput(Vector2.zero);
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

        public void RegisterCallbackAttack(Action callbackHandler)
        {
            _attackPressed += callbackHandler;
        }

        public void UnRegisterCallbackAttack(Action callbackHandler)
        {
            _attackPressed -= callbackHandler;
        }
    }
}
