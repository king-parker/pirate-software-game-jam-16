using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static string TAG = "AudioManager";

    private static AudioManager m_instance;

    private float m_levelWidth = 0;

    // Event Instances
    private FMOD.Studio.EventInstance m_musicInstance;

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
    private const string SPEED = "speed";
    private const string TARGET_POSITION = "Target Position";

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
        RuntimeManager.LoadBank("Master");
        RuntimeManager.LoadBank("Master.strings");
        RuntimeManager.LoadBank(SOUNDTRACK);
        RuntimeManager.LoadBank(SFX);

        // Wait until the banks are fully loaded
        while (!RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }

        // Preload sample data
        var bankName = BANK_PREFIX + SOUNDTRACK;
        Bank soundtrackBank;
        RuntimeManager.StudioSystem.getBank(bankName, out soundtrackBank);
        soundtrackBank.loadSampleData();

        LOADING_STATE loadingState;
        var result = soundtrackBank.getSampleLoadingState(out loadingState);
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

    public void UpdateLevelWidth()
    {
        m_levelWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
    }

    /** One-Shot SFX **/
    public void PlayGunshot(float position)
    {
        UpdateGlobalParameterHelper(GUN_POSITION, NormalizePosition(position));
        PlaySound(EVENT_PREFIX + GUNSHOT);
    }

    /** One-Shot SFX **/
    public void PlayGunCollision(float position, float speed)
    {
        speed = Mathf.Clamp(Mathf.Abs(speed), 0f, 20f);

        UpdateGlobalParameterHelper(GUN_POSITION, NormalizePosition(position));
        EventInstance soundInstance = RuntimeManager.CreateInstance(EVENT_PREFIX + GUN_COLLISION);
        UpdateEventParameterHelper(soundInstance, SPEED, speed);
        soundInstance.start();
        soundInstance.release();
    }

    /** One-Shot SFX **/
    public void PlayTargetBreak(float position)
    {
        EventInstance soundInstance = RuntimeManager.CreateInstance(EVENT_PREFIX + TARGET_BREAK);
        UpdateEventParameterHelper(soundInstance, TARGET_POSITION, NormalizePosition(position));
        soundInstance.start();
        soundInstance.release();
    }

    /** Play One-Shot Helpers **/
    private void PlaySound(string eventPath)
    {
        RuntimeManager.PlayOneShot(eventPath);
    }

    /** Bullet Time Mix **/
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

    private void UpdateBulletTimeParameter(bool isBulletTimeOn)
    {
        UpdateGlobalParameterHelper(BULLET_TIME_PARAMETER, FlagValue(isBulletTimeOn));
    }

    /** Stage  Mix **/
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

    private void UpdateStageClearParameter(bool isStageCleared)
    {
        UpdateEventParameterHelper(m_musicInstance, STAGE_CLEAR_PARAMETER, isStageCleared);
    }

    /** Stage Start **/
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

    private void UpdateStageStartParameter(bool isStageStart)
    {
        UpdateEventParameterHelper(m_musicInstance, STAGE_START_PARAMETER, isStageStart);
    }

    /** Parameter Helpers **/
    private float FlagValue(bool flag) { return (flag ? 1f : 0f); }

    private float NormalizePosition(float position) {  return position / m_levelWidth; }

    private void UpdateEventParameterHelper(EventInstance eventInstance, string parameterName, bool flag)
    {
        eventInstance.setParameterByName(parameterName, FlagValue(flag));
    }

    private void UpdateEventParameterHelper(EventInstance eventInstance, string parameterName, float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }

    private void UpdateGlobalParameterHelper(string parameterName, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }
}
