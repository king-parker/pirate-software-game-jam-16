using static LevelManager;

public class LevelLoadUtility
{
    public static void LoadLevelFromMenu(LevelManager.GameLevel level, LevelManager levelManager, AudioManager musicManager)
    {
        musicManager.StageStart();
        levelManager.LoadLevel(level);
    }
}