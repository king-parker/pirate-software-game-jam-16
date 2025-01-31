using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public static string TAG = "PauseScreen";

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private GunController player;

    private AudioManager m_musicManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();

        resumeButton.onClick.AddListener(() => Resume());
        restartButton.onClick.AddListener(() => RestartLevel());
        mainMenuButton.onClick.AddListener(() => MainMenu());
        levelSelectButton.onClick.AddListener(() => LevelSelect());
    }

    public void Resume()
    {
        player.Unpause();
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        MenuLoadUtility.LoadMainMenu(m_musicManager);
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
        MenuLoadUtility.LoadLevelSelect(m_musicManager);
    }
}
