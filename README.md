Aspirobot
=========
Aspirobot is a simple(r) AI agent that vaccums dust in your rooms but leaves your jewels.

Build
-----
- Command line  
`cd uqac-ia-aspirobot
dotnet restore
dotnet build
dotnet run`

- Visual Studio  
Run VS, open solution, wait for package restore, click run (F5)

- Rider  
Run Rider, open solution, wait for package restore, click run (Shift + F10)

Configuration
-------------
Some behaviors can be configured in Program.cs file:  
- EnvConfig (Environment configuration)
  - Height: Map height
  - Width: Map width
  - ActionPropability: Probability that an action occurs in the enviromnent, once per SleepTime (>=0, <=100)
  - AddDustProbability: Probability that a dust is added on the map, only when an action is to be taken (>=0, <=100)
  - AddJewelProbability: Probability that a jewel is added on the map, only when an action is to be taken, and AddDustProbability does not occur (>=0, <=100)
  - RemoveJewelProbability: Probability that a jewel is removed from the map, only when an action is to be taken, and neither AddDustProbability nor AddJewelProbability occur (>=0, <=100)
  - SleepTime: Time between each iteration in ms (>=0)
  
- AgConfig (Agent configuration)
  - SleepTime: Time between each iteration in ms (>=0)
  - StartX: Agent start X position (>=0, <EnvConfig.Width)
  - StartY: Agent start Y position (>=0, <EnvConfig.Height)
  - AutoAdjustThinkTimeInterval: Indicate if agent should try to auto adjust think time interval 
  - ThinkTimeInterval: Default think time interval in ms (>=0). This variable controls how often the agent will re-evaluate the situation
  
How it works
------------
