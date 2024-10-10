namespace Framework.Tool.StateMachine
{
    public abstract class StateMachine<T>
    {
        //当前状态  
        private IState<T> _curState;

        //下一个状态  
        private IState<T> _nextState;

        //下一个状态进入参数  
        private object[] _nextStateParams;

        //状态列表  
        private IState<T>[] _stateList;


        //初始化状态机，第一个状态为默认状态  
        public StateMachine(T obj,IState<T> initialState)
        {
            this._curState = initialState;
            this._curState.onStateEnter();
        }
    }
}