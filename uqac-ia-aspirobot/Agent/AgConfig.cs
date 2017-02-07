namespace uqac_ia_aspirobot.Agent
{
    public class AgConfig
    {
        public bool AutoAdjustSleepTime { get; set; }

        public int SleepTime { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public void CopyTo(AgConfig other)
        {
            other.SleepTime = SleepTime;
            other.StartX = StartX;
            other.StartY = StartY;
        }
    }
}