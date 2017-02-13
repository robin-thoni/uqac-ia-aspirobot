namespace uqac_ia_aspirobot.Agent
{
    public class AgConfig
    {
        public bool AutoAdjustThinkTimeInterval { get; set; }

        public int SleepTime { get; set; }

        public int ThinkTimeInterval { get; set; }

        public float LowPerformance { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public void CopyTo(AgConfig other)
        {
            other.AutoAdjustThinkTimeInterval = AutoAdjustThinkTimeInterval;
            other.SleepTime = SleepTime;
            other.ThinkTimeInterval = ThinkTimeInterval;
            other.LowPerformance = LowPerformance;
            other.StartX = StartX;
            other.StartY = StartY;
        }
    }
}