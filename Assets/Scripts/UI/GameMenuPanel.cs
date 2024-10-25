using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuPanel : BasePanel
{
    public void ContinueButtonClick()
    {
        UIManager.Instance.HidePanel("GameMenuPanel");
    }

    public void ExitButtonClick()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("开始场景");
    }
}
