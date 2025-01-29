using static LevelManager;

public class LevelLoadUtility
{
    public static void LoadLevelFromMenu(LevelManager.GameLevel level, LevelManager levelManager, MusicManager musicManager)
    {
        musicManager.StageStart();
        levelManager.LoadLevel(level);
    }
}