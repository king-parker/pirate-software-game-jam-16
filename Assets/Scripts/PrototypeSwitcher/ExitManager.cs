using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private string selectScene = "PrototypeSelect";

    private KeyCode m_exitKey = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKeyDown(m_exitKey))
        {
            SceneManager.LoadScene(selectScene);
        }
    }
}
