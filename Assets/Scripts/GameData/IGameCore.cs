namespace IM.GameData
{
    public interface IGameCore
    {
        void StartGame();
        void RespawnPlayer();
        void UpdateHighScore();
        void RestartGame();
        void UpdateCurrentScore(int val);
    }
}