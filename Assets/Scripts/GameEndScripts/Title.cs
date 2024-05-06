using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void LoadFirstScene()
    {
        EnvironmentManager.InitializeEnvironment();
        SceneManager.LoadScene("0Lab");
    }
}
