namespace Tools.StateMachine
{
    public interface IState
    {
        public int ID { get; }
        public void Enter();
        public void Update(float deltaTime);
        public void FixedUpdate(float fixedDeltaTime);
        public void Exit();

    }
}
