using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    private MusicManager m_musicManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(MusicManager.TAG).GetComponent<MusicManager>();

        startGameButton.onClick.AddListener(() => StartGame());
    }

    private void StartGame()
    {
        m_musicManager.StartMusic();

        SceneManager.LoadScene(MenuNames.MainMenu);
    }
}
