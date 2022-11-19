namespace MyTime.Models.StopWatches
{
    public interface IStopWatchesWrapper
    {
        IStopWatch PauseStopWatch { get; }
        IStopWatch TaskStopWatch { get; }
        IStopWatch WorkTimeStopWatch { get; }
    }
}