using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Level 1");
        SceneManager.LoadScene("DontDestroyOnLoad", LoadSceneMode.Additive);
    }
}
