using UnityEngine.SceneManagement;

public static class MenuLoadUtility
{
    public static void LoadMainMenu(MusicManager musicManager)
    {
        MenuLoad(MenuNames.MainMenu, musicManager);
    }

    public static void LoadLevelSelect(MusicManager musicManager)
    {
        MenuLoad(MenuNames.LevelSelect, musicManager);
    }

    private static void MenuLoad(string menuName, MusicManager musicManager)
    {
        musicManager.StopStageClearMix();
        musicManager.SwitchToMenu();

        SceneManager.LoadScene(menuName);
    }
}