using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelManager.GameLevel gameLevel;

    private MusicManager m_musicManager;
    private LevelManager m_levelManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(MusicManager.TAG).GetComponent<MusicManager>();
        m_levelManager = GameObject.FindGameObjectWithTag(LevelManager.TAG).GetComponent<LevelManager>();

        GetComponent<Button>().onClick.AddListener(() => LoadLevel());
    }

    private void LoadLevel()
    {
        LevelLoadUtility.LoadLevelFromMenu(gameLevel, m_levelManager, m_musicManager);
    }
}
