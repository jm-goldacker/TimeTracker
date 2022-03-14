namespace MyTime.Models.Database
{
    public class PauseTime : BaseTime
    {
        public virtual WorkTime WorkTime { get; set; }
    }
}
