using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    private AudioManager m_musicManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();

        startGameButton.onClick.AddListener(() => StartGame());

        StartCoroutine(m_musicManager.LoadBanks());
    }

    private void StartGame()
    {
        m_musicManager.InitializeMusic();

        SceneManager.LoadScene(MenuNames.MainMenu);
    }
}
