namespace uqac_ia_aspirobot.Agent
{
    public class AgBatterySensor
    {
        public int Spent { get; protected set; }

        public void Add(int v)
        {
            if (v > 0)
            {
                Spent += v;
            }
        }
    }
}