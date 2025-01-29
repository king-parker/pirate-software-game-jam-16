using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameLevel[] levels;

    public static string TAG = "LevelManager";

    private static LevelManager m_instance;

    private int m_currentLevelIndex = 0;

    public enum GameLevel
    {
        FirstShots,
        AStepUp
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureLevelManagerExists()
    {
        if (m_instance == null)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Level Manager");
            if (prefab != null)
            {
                Instantiate(prefab);
            }
        }
    }

    public GameLevel GetFirstLevel()
    {
        return levels[0];
    }

    public bool IsLastLevel()
    {
        return m_currentLevelIndex + 1 >= levels.Length;
    }

    public void LoadNextLevel()
    {
        m_currentLevelIndex++;

        if (m_currentLevelIndex < levels.Length)
        {
            LoadLevel(levels[m_currentLevelIndex]);
        }
        else
        {
            throw new System.Exception("Tried to load next level when there was no next level");
        }
    }

    public void LoadLevel(GameLevel level)
    {
        var levelIndex = System.Array.IndexOf(levels, level);
        if (levelIndex >= 0)
        {
            m_currentLevelIndex = levelIndex;
            SceneManager.LoadScene(level.ToString());
        }
        else
        {
            throw new System.Exception($"Level {level} not found in levels list");
        }
    }
}
