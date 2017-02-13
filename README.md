Aspirobot
=========
Aspirobot is a simple(r) AI agent that vaccums dust in your rooms but pick up your jewels.

Build
-----
- Command line  
```shell
cd uqac-ia-aspirobot
dotnet restore
dotnet build
dotnet run
```

- Visual Studio  
Run VS, open solution, wait for package restore, click run (F5)

- Rider  
Run Rider, open solution, wait for package restore, click run (Shift + F10)

Configuration
-------------
Some behaviors can be configured in Program.cs file:  
- `EnvConfig` (Environment configuration)
  - `Height`: Map height
  - `Width`: Map width
  - `ActionPropability`: Probability that an action occurs in the enviromnent, once per `EnvConfig.SleepTime` (>=0, <=100)
  - `AddDustProbability`: Probability that a dust is added on the map, only when an action is to be taken (>=0, <=100)
  - `RemoveJewelProbability`: Probability that a jewel is removed from the map, only when an action is to be taken,
  and `EnvConfig.AddDustProbability` does not occur (>=0, <=100)
  - `AddJewelProbability`: Probability that a jewel is added on the map, only when an action is to be taken,
  and neither `EnvConfig.AddDustProbability` nor `EnvConfig.RemoveJewelProbability` occur (>=0, <=100)
  - `SleepTime`: Time between each iteration in ms (>=0)
  
- `AgConfig` (Agent configuration)
  - `SleepTime`: Time between each iteration in ms (>=0)
  - `StartX`: Agent start X position (>=0, <`EnvConfig.Width`)
  - `StartY`: Agent start Y position (>=0, <`EnvConfig.Height`)
  - `AutoAdjustThinkTimeInterval`: Indicate if agent should try to auto adjust think time interval (`AgConfig.ThinkTimeInterval`)
  - `ThinkTimeInterval`: Default think time interval in ms (>=0). This variable controls how often the agent will re-evaluate the situation
  
How it works
------------
Agent will loop forever (until killed) peforming the following operations:
- Environment update: this action will force all sensors to update their data, so that the environment can be updated.
  - In the FakeEnv implementation, the environment update itself from remote virtual environment, so the sensors updates do nothing.
  - In a real robot implementation, the update methods of sensors would gather data and push them to the environment.
- UI update: this will update the user interface.
  - In the FakeEnv implementation, it draw in the standard output.
  - In a real robot implementation, it would either do nothing, or update a web interface (for example)
- Internal state update: Prepare internal state to be able to choose an action to do.
- Choose an action: Choose the most appropriate action to do by analysing the gathered data and current internal state.
- Act: Execute choosen action (go to room, vaccum, pick)
- Sleep: Sleep for `AgConfig.SleepTime`ms

FakeEnv implementation will loop forever (until killed) peforming the following operations:
- Sleep: Sleep for `EnvConfig.SleepTime`ms
- May do something: Randomly choose to do something or not using `EnvConfig.ActionPropability`
    - May add dust on a random location: Randomly choose to do it or not using `EnvConfig.AddDustProbability`
    - May remove a jewel from the first room that contains a jewel: Randomly choose to do it or not using `EnvConfig.RemoveJewelProbability`.
    Can only happen if no previous action has been done.
    - May add a jewel on a random location: Randomly choose to do it or not using `EnvConfig.AddJewelProbability`.
    Can only happen if no previous action has been done.

Code
----
This agent is coded in .NET Core (C#) using DI (Dependency Injection) and interfaces to abstract implementation
and allow to run a virtual environment or a real one without changing anything in the agent implementation

FakeEnv implementation starts one thread for the environment and another one for the agent.

Agent logic is in `Agent/*.cs`  
Agent sensors and effectors interfaces are in `Agent/Interfaces/*`  
Agent FakeEnv client implementation is in `Agent/FakeEnv/*`  
FakeEnv client, server, protocol and configuration are defined in `Common/*.cs`  
FakeEnv server implementation is in `Environment/*.cs`  
Some helper methods are defined in `Extensions/Extensions.cs`  
Shared interfaces are in `Interfaces/*.cs`  
Console user interface is in `UI/UiConsole.cs`