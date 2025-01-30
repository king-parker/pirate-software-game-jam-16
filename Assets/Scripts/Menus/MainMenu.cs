using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button levelSelectButton;

    private AudioManager m_musicManager;
    private LevelManager m_levelManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();
        m_levelManager = GameObject.FindGameObjectWithTag(LevelManager.TAG).GetComponent<LevelManager>();

        startGameButton.onClick.AddListener(() => StartGame());
        levelSelectButton.onClick.AddListener(() => LevelSelect());
    }

    private void StartGame()
    {
        LevelLoadUtility.LoadLevelFromMenu(m_levelManager.GetFirstLevel(), m_levelManager, m_musicManager);
    }

    private void LevelSelect()
    {
        SceneManager.LoadScene(MenuNames.LevelSelect);
    }
}
