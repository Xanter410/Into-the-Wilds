using System;
using System.Collections.Generic;
using Tools.BehaviorTree;
using UnityEngine;
using VContainer.Unity;

namespace IntoTheWilds
{
    public class GoblinTorchAI : BehaviorTree, ITickable, IStartable, IMove, IAttack, IStunble
    {
        public event Action AttackPressed;

        private readonly Transform _transform;

        private Vector2 _spawnPoint;
        private bool _isStunned = false;

        private Vector2 _moveInput = Vector2.zero;

        public GoblinTorchAI(Rigidbody2D unitRigidbody2D)
        {
            _transform = unitRigidbody2D.transform;
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
                            new AI_CheckTargetInRange(_transform, 1f),
                            new AI_Attack(this, 1f)
                        }),

                        new Sequence(new List<Node>
                        {
                            new AI_CheckDistanceToSpawnPoint(_transform, _spawnPoint, 10f, 5f),
                            new AI_MoveToTarget(this, _transform, 0.7f)
                        })
                    })

                }),

                new AI_IdleWalkNearSpawn(this, _transform, _spawnPoint, 3f, 3f)
            }); 

            return root;
        }

        public Vector2 RetrieveMoveInput()
        {
            return _moveInput;
        }

        void IAttack.OnAttackPressed()
        {
            ((IMove)this).SetMoveInput(Vector2.zero);

            AttackPressed?.Invoke();
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
                ((IMove)this).SetMoveInput(Vector2.zero);
            }
            else
            {
                _isStunned = false;
            }
        }
    }
}
