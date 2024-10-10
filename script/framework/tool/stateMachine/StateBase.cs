
namespace Framework.Tool.StateMachine
{
    public abstract class StateBase<T> : IState<T>
    {
        public T owner;
        public StateMachine<T> machine;

        public virtual void onStateEnter()
        {
        }
        
        public virtual void onCreate()
        {
        }

        public virtual void onStateUpdate(float dt)
        {
        }

        public virtual void onStateExit()
        {
        }

        public virtual string stateName()
        {
            return GetType().Name;
        }
    }
}