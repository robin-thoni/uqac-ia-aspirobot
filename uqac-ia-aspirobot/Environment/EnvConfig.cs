namespace uqac_ia_aspirobot.Environment
{
    public class EnvConfig
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public float ActionPropability { get; set; }

        public float AddJewelProbability { get; set; }

        public float RemoveJewelProbability { get; set; }

        public float AddDustProbability { get; set; }

        public int SleepTime { get; set; }

        public void CopyTo(EnvConfig other)
        {
            other.Width = Width;
            other.Height = Height;
            other.ActionPropability = ActionPropability;
            other.AddJewelProbability = AddJewelProbability;
            other.RemoveJewelProbability = RemoveJewelProbability;
            other.AddDustProbability = AddDustProbability;
            other.SleepTime = SleepTime;
        }
    }
}