using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{
    public void NewGame()
    {
        // SceneManager.LoadScene("第一张场景");
    }

    public void Continue()
    {
        string lastScene = PlayerPrefs.GetString("LastScene");
        SceneManager.LoadScene(lastScene);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
