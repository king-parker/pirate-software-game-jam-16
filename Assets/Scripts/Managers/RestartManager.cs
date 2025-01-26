using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    [SerializeField] private string currentScene = "GunPrototype";

    private KeyCode m_exitKey = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKeyDown(m_exitKey))
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
