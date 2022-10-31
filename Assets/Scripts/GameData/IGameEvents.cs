using System;

namespace IM.GameData
{
    public interface IGameEvents
    {
        event Action<int> OnScoreChanged;
        event Action<int> OnHighScoreUpdated;

        event Action OnRespawn;
        event Action OnReset;
    }
}