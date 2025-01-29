using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button levelSelectButton;

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
            messageText.text = "Final " + messageText.text;
        }
        else
        {
            nextLevelButton.onClick.AddListener(() => NextLevel());
        }
        mainMenuButton.onClick.AddListener(() => MainMenu());
        levelSelectButton.onClick.AddListener(() => LevelSelect());
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

    public void MainMenu()
    {
        MenuLoadUtility.LoadMainMenu(m_musicManager);
    }

    public void LevelSelect()
    {
        MenuLoadUtility.LoadLevelSelect(m_musicManager);
    }
}
