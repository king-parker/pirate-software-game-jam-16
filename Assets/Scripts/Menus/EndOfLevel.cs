using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private string currentScene;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() => RestartLevel());
        exitButton.onClick.AddListener(() => ExitLevel());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    // TODO: This should probably not exist in a web game
    // Other buttons to be added are Main Menu and Level Select
    public void ExitLevel()
    {
        Application.Quit();
    }
}
