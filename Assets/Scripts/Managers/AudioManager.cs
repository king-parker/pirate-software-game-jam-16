using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static string TAG = "MusicManager";

    private static AudioManager m_instance;

    // Event Instances
    private FMOD.Studio.EventInstance m_musicInstance;
    private FMOD.Studio.EventInstance m_gunshotInstance;
    private FMOD.Studio.EventInstance m_gunCollisionInstance;
    private FMOD.Studio.EventInstance m_targetBreakInstance;

    // Prefix strings
    private const string EVENT_PREFIX = "event:/";
    private const string BANK_PREFIX = "bank:/";

    // For both the event and bank
    private const string SOUNDTRACK = "Soundtrack";

    // All SFX strings
    private const string SFX = "SFX";
    private const string GUN_COLLISION = "Gun Collision";
    private const string GUNSHOT = "Gunshot 2";
    private const string TARGET_BREAK = "Target-Break";

    // Parameter Strings
    private const string BULLET_TIME_PARAMETER = "Bullet Time Mix";
    private const string STAGE_CLEAR_PARAMETER = "Stage Clear Mix";
    private const string STAGE_START_PARAMETER = "Stage Start";
    private const string GUN_POSITION = "Gun Position";

    private void Awake()
    {
        if (m_instance == null)
        {
            // If first instance, start music
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Prevent duplicate instance
            Destroy(gameObject);
        }
    }

    public void InitializeMusic()
    {
        var eventName = EVENT_PREFIX + SOUNDTRACK;
        m_musicInstance = RuntimeManager.CreateInstance(eventName);
        m_musicInstance.start();
    }

    public System.Collections.IEnumerator LoadBanks()
    {
        // Load banks
        RuntimeManager.LoadBank(SOUNDTRACK);

        // Wait until the banks are fully loaded
        while (!RuntimeManager.HasBankLoaded(SOUNDTRACK))
        {
            yield return null;
        }

        // Preload sample data
        var bankName = BANK_PREFIX + SOUNDTRACK;
        Bank soundtrackBank;
        RuntimeManager.StudioSystem.getBank(bankName, out soundtrackBank);
        soundtrackBank.loadSampleData();
    }

    private void OnDestroy()
    {
        if (m_musicInstance.isValid())
        {
            m_musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        UpdateGlobalParameterHelper(BULLET_TIME_PARAMETER, FlagValue(isBulletTimeOn));
    }

    private void UpdateStageClearParameter(bool isStageCleared)
    {
        UpdateEventParameterHelper(m_musicInstance, STAGE_CLEAR_PARAMETER, isStageCleared);
    }

    private void UpdateStageStartParameter(bool isStageStart)
    {
        UpdateEventParameterHelper(m_musicInstance, STAGE_START_PARAMETER, isStageStart);
    }

    private float FlagValue(bool flag) { return (flag ? 1f : 0f); }

    private void UpdateEventParameterHelper(EventInstance eventInstance, string parameterName, bool flag)
    {
        eventInstance.setParameterByName(parameterName, FlagValue(flag));
    }

    private void UpdateGlobalParameterHelper(string parameterName, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }
}
