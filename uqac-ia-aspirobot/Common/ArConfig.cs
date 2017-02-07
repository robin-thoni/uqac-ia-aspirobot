namespace uqac_ia_aspirobot.Common
{
    public class ArConfig
    {
        public string PipeName { get; set; }

        public string PipeServer { get; set; }

        public int ServerThreadCount { get; set; }

        public void CopyTo(ArConfig other)
        {
            other.PipeName = PipeName;
            other.PipeServer = PipeServer;
            other.ServerThreadCount = ServerThreadCount;
        }
    }
}