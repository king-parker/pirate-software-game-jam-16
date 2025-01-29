using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private string musicName = "event:/Soundtrack";
    [SerializeField] private bool isStage = false;

    public static string TAG = "MusicManager";

    private static MusicManager m_instance;
    private FMOD.Studio.EventInstance m_musicInstance;

    private const string BULLET_TIME_PARAMETER = "Bullet Time Mix";
    private const string STAGE_CLEAR_PARAMETER = "Stage Clear Mix";
    private const string STAGE_START_PARAMETER = "Stage Start";

    private void Awake()
    {
        if (m_instance == null)
        {
            // If first instance, start music
            m_instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMusic();
        }
        else
        {
            // Prevent duplicate instance
            Destroy(gameObject);
        }
    }

    private void InitializeMusic()
    {
        // Create the music instance
        m_musicInstance = RuntimeManager.CreateInstance(musicName);

        // Preload the sample data to prevent audio delay
        FMOD.Studio.EventDescription eventDesc;
        RuntimeManager.StudioSystem.getEvent(musicName, out eventDesc);
        eventDesc.loadSampleData();
    }

    private void OnDestroy()
    {
        if (m_musicInstance.isValid())
        {
            StopMusic();
            m_musicInstance.release();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureMusicManagerExists()
    {
        if (m_instance == null)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Music Manager");
            if (prefab != null)
            {
                Instantiate(prefab);
            }
        }
    }

    [ContextMenu("Start Music")]
    public void StartMusic()
    {
        if (isStage) { StageStart(); }

        m_musicInstance.start();
    }

    [ContextMenu("Stop Music")]
    public void StopMusic()
    {
        m_musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    [ContextMenu("Start Bullet Time Mix")]
    public void StartBulletTimeMix()
    {
        UpdateBulletTimeParameter(true);
    }

    [ContextMenu("Stop Bullet Time Mix")]
    public void StopBulletTimeMix()
    {
        UpdateBulletTimeParameter(false);
    }

    [ContextMenu("Start Stage Clear Mix")]
    public void StartStageClearMix()
    {
        UpdateStageClearParameter(true);
    }

    [ContextMenu("Stop Stage Clear Mix")]
    public void StopStageClearMix()
    {
        UpdateStageClearParameter(false);
    }

    [ContextMenu("Stage Start")]
    public void StageStart()
    {
        UpdateStageStartParameter(true);
    }

    [ContextMenu("Switch to Menu")]
    public void SwitchToMenu()
    {
        UpdateStageStartParameter(false);
    }

    private void UpdateBulletTimeParameter(bool isBulletTimeOn)
    {
        UpdateParameterHelper(BULLET_TIME_PARAMETER, isBulletTimeOn);
    }

    private void UpdateStageClearParameter(bool isStageCleared)
    {
        UpdateParameterHelper(STAGE_CLEAR_PARAMETER, isStageCleared);
    }

    private void UpdateStageStartParameter(bool isStageStart)
    {
        UpdateParameterHelper(STAGE_START_PARAMETER, isStageStart);
    }

    private void UpdateParameterHelper(string parameterName, bool flag)
    {
        var value = flag ? 1f : 0f;

        m_musicInstance.setParameterByName(parameterName, value);
    }
}
