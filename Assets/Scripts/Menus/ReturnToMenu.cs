using UnityEngine;
using UnityEngine.UI;

public class ReturnToMenu : MonoBehaviour
{
    private void Start()
    {
        var musicManager = GameObject.FindGameObjectWithTag(MusicManager.TAG).GetComponent<MusicManager>();

        GetComponent<Button>().onClick.AddListener(() => MenuLoadUtility.LoadMainMenu(musicManager));
    }
}
