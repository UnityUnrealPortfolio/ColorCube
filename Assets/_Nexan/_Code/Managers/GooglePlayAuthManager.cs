using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayAuthManager :SingletonParent<GooglePlayAuthManager>
{
    private bool m_IsConnectedToGooglePlay;
    public bool IsConnectedToGooglePlay { get => m_IsConnectedToGooglePlay; }
    protected override void Awake()
    {
        base.Awake();
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate(HandleLogin);
    }

    private void HandleLogin(SignInStatus status)
    {
        if(status == SignInStatus.Success)
        {
           m_IsConnectedToGooglePlay = true;
        }
        else
        {
            m_IsConnectedToGooglePlay = false;
        }
    }
}
