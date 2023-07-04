using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using TMPro;

public class LeaderboardsManager : SingletonParent<LeaderboardsManager>
{
    public GameObject leaderBoardCanvas;
    public TMP_Text leaderBoardDebugText;
    protected override void Awake()
    {
        base.Awake();
        PlayGamesPlatform.Activate();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called from GameManager to Add score to leaderboard
    /// </summary>
    /// <param name="_score"></param>
    public void AddScoreToLeaderboard(int _score)
    {
        if(GooglePlayAuthManager.Instance.IsConnectedToGooglePlay == true)
        {
            Social.ReportScore(_score, GPGSIds.leaderboard_flippincube, HandleLeaderBoardUpdate);
        }
    }
    public void HandleShowLeaderBoard()
    {
        if(GooglePlayAuthManager.Instance.IsConnectedToGooglePlay == true)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            GooglePlayAuthManager.Instance.LoginGooglePlayGames();
        }
    }
    private void HandleLeaderBoardUpdate(bool _result)
    {
        //leaderBoardDebugText.text = "Leaderboard updated successfully";
    }
}
