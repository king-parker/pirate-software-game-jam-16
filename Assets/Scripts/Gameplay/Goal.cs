using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GunController player;
    [SerializeField] private GameObject endOfLevelScreen;

    private AudioManager m_musicManager;

    private void Start()
    {
        m_musicManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();
    }

    public void GoalReached()
    {
        endOfLevelScreen.SetActive(true);
        m_musicManager.StartStageClearMix();
        player.DisableInputs();
    }
}
