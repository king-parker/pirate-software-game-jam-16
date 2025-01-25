using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrototypeSelectManager : MonoBehaviour
{
    [SerializeField] private Button parasiteButton;
    [SerializeField] private string parasiteScene = "ParasitePrototype";
    [SerializeField] private Button gunButton;
    [SerializeField] private string gunScene = "GunPrototype";

    private void Start()
    {
        parasiteButton.onClick.AddListener(() => LoadScene(parasiteScene));
        gunButton.onClick.AddListener(() => LoadScene(gunScene));
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
