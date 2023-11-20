namespace Timer.ElapsedTimeStructure
{
    public class ElapsedTime
    {
        public int ElapsedSeconds { get;}
        public int ElapsedMinutes { get;}
        public int ElapsedHours { get;}
        public int ElapsedDays { get;}
        
        public ElapsedTime(int elapsedSeconds, int elapsedMinutes, int elapsedHours, int elapsedDays)
        {
            ElapsedSeconds = elapsedSeconds;
            ElapsedMinutes = elapsedMinutes;
            ElapsedHours = elapsedHours;
            ElapsedDays = elapsedDays;
        }
    }
}