using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private string currentScene;
    [SerializeField] private Button exitButton;

    private MusicManager m_musicManager;

    private void Start()
    {
        restartButton.onClick.AddListener(() => RestartLevel());
        exitButton.onClick.AddListener(() => ExitLevel());

        m_musicManager = GameObject.FindGameObjectWithTag(MusicManager.TAG).GetComponent<MusicManager>();
    }

    public void RestartLevel()
    {
        m_musicManager.StopStageClearMix();
        SceneManager.LoadScene(currentScene);
    }

    // TODO: This should probably not exist in a web game
    // Other buttons to be added are Main Menu and Level Select
    public void ExitLevel()
    {
        Application.Quit();
    }
}
