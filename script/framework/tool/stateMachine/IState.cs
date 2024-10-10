namespace Framework.Tool.StateMachine
{
    public interface IState<T>
    {
        /**
         * 进入状态
         * @param params
         */
        void onStateEnter();

        /**
         * 创建时
         * @param params
         */
        void onCreate();

        /**
        * 更新
        * @param params
        */
        void onStateUpdate(float dt);

        /**
         * 退出状态
         */
        void onStateExit();

        /**
         * 当前状态名字
         */
        string stateName();
    }
}