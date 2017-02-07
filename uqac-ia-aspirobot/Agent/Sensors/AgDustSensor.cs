﻿using uqac_ia_aspirobot.Interfaces;

namespace uqac_ia_aspirobot.Agent
{
    public class AgDustSensor
    {
        private readonly AgEnvironment _environment;

        public AgDustSensor(IEnvironment environment)
        {
            _environment = environment as AgEnvironment;
        }

        public void Update()
        {
            _environment.Update();
        }
    }
}