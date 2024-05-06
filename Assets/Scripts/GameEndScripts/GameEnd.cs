using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public void LoadTitletScene()
    {
        SceneManager.LoadScene("Title");
    }
}
