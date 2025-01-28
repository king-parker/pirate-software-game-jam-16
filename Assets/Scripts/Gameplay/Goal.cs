using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GunController player;
    [SerializeField] private GameObject endOfLevelScreen;

    public void GoalReached()
    {
        endOfLevelScreen.SetActive(true);
        player.DisableInputs();
    }
}
