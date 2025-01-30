using UnityEngine.SceneManagement;

public static class MenuLoadUtility
{
    public static void LoadMainMenu(AudioManager musicManager)
    {
        MenuLoad(MenuNames.MainMenu, musicManager);
    }

    public static void LoadLevelSelect(AudioManager musicManager)
    {
        MenuLoad(MenuNames.LevelSelect, musicManager);
    }

    private static void MenuLoad(string menuName, AudioManager musicManager)
    {
        musicManager.StopStageClearMix();
        musicManager.SwitchToMenu();

        SceneManager.LoadScene(menuName);
    }
}