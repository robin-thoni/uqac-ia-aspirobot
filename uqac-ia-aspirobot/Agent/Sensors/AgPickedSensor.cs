namespace uqac_ia_aspirobot.Agent
{
    public class AgPickedSensor
    {
        public int Picked { get; protected set; }

        public void Increase()
        {
            ++Picked;
        }
    }
}