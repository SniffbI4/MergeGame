namespace Scripts.Game
{
    public interface IGameListener
    {
        
    }

    public interface IGameInitListener : IGameListener
    {
        void OnGameInit();
    }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnGamePaused();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnGameResumed();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinished();
    }

    public interface IGameRestartListener : IGameListener
    {
        void OnGameRestarted();
    }

    public interface IUpdateListener : IGameListener
    {
        void Update(float deltaTime);
    }

    public interface IFixedUpdateListener : IGameListener
    {
        void FixedUpdate(float deltaTime);
    }
}