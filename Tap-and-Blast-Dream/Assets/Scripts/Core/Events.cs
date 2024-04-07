using System;
using System.Collections.Generic;

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
        public static Action<Blastable> OnPlayerInput;
        public static Action<List<BoardEntity>, Blastable> OnBlast;
        public static Action<bool> OnGameOver;
        public static Action OnTryAgain;
        public static Action OnQuitLevel;

        public static Action<bool> OnNotifyGameEnd;
    }
}
