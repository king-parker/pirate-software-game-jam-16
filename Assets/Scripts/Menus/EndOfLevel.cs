using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button exitButton;

    private MusicManager m_musicManager;
    private LevelManager m_levelManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(MusicManager.TAG).GetComponent<MusicManager>();
        m_levelManager = GameObject.FindGameObjectWithTag(LevelManager.TAG).GetComponent<LevelManager>();

        restartButton.onClick.AddListener(() => RestartLevel());
        if (m_levelManager.IsLastLevel())
        {
            nextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            nextLevelButton.onClick.AddListener(() => NextLevel());
        }
        exitButton.onClick.AddListener(() => ExitLevel());
    }

    public void RestartLevel()
    {
        m_musicManager.StopStageClearMix();

        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void NextLevel()
    {
        m_levelManager.LoadNextLevel();
    }

    // TODO: This should probably not exist in a web game
    // Other buttons to be added are Main Menu and Level Select
    public void ExitLevel()
    {
        Application.Quit();
    }
}
