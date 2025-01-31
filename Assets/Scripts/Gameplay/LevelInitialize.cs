using UnityEngine;

public class LevelInitialize : MonoBehaviour
{
    private void Start()
    {
        // Tell AudioManager to get an updated level width for 3D audio placement
        var audioManager = GameObject.FindGameObjectWithTag(AudioManager.TAG).GetComponent<AudioManager>();
        audioManager.UpdateLevelWidth();

        // Done initializing, destroy object
        Destroy(this.gameObject);
    }
}
