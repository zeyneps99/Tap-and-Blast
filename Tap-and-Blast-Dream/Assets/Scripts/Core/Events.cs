using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Events
{
    public static class CoreEvents
    {
        public static Action<Command> OnCommand;
    }
    public static class GameEvents
    {
        public static Action OnMainMenuStarted;
        public static Action OnPlay;
        public static Action<LevelInfo> OnLevelGenerated;
    }
}
