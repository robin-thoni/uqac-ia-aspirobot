namespace uqac_ia_aspirobot.Agent
{
    public class AgVaccumSensor
    {
        public int Vaccumed { get; protected set; }

        public void Increase()
        {
            ++Vaccumed;
        }
    }
}