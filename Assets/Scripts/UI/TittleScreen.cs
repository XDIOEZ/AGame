using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("第一张场景");
    }

    public void Continue()
    {
        Debug.Log("TODO: 读取保存的场景名称，加载对应场景");
        //TODO: 读取保存的场景名称，加载对应场景
        // SceneManager.LoadScene("SavedSceneName");
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
