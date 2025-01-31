using UnityEngine;
using UnityEngine.UI;

public class ReturnToMenu : MonoBehaviour
{
    private void Start()
    {
        var musicManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();

        GetComponent<Button>().onClick.AddListener(() => MenuLoadUtility.LoadMainMenu(musicManager));
    }
}
