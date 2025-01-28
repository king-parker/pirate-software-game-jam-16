using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GunController player;
    [SerializeField] private GameObject endOfLevelScreen;

    private MusicManager m_musicManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(MusicManager.TAG).GetComponent<MusicManager>();
    }

    public void GoalReached()
    {
        endOfLevelScreen.SetActive(true);
        m_musicManager.StartStageClearMix();
        player.DisableInputs();
    }
}
