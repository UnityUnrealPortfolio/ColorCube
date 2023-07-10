using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonParent<SceneController>
{
    protected override void Awake()
    {
        base.Awake();
        
    }
    public void HandleLearnButton()
    {
        AnalyticsManager.Instance.SendVisitHomePageEvent();
        Application.OpenURL("https://Nexan.org");
    }

    public void HandlePlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void HandleBackButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
