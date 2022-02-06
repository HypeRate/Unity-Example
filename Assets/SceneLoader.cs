using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        Debug.Log(sceneName);
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(sceneName);
    }
}
