namespace Model.Extensions.Pause
{
    public interface IPauseService
    {
        public bool IsPause { get; }

        public void SetPaused(bool isPaused);

        public void AddPauseHandler(IPauseHandler pauseHandler);

        public void RemovePauseHandler(IPauseHandler pauseHandler);
    }
}