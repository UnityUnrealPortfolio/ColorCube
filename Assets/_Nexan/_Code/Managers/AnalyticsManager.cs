using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsManager : SingletonParent<AnalyticsManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void SendVisitHomePageEvent()
    {
        string timeStamp = DateTime.Now.ToString();
        Dictionary<string, object> visitHomeData = new Dictionary<string, object>()
        {
            {"homevisitstamp",timeStamp}
        };

        AnalyticsService.Instance.CustomData("visithome", visitHomeData);
        AnalyticsService.Instance.Flush();
    }

    public void SendScoreAtDeathEvent(int _score)
    {
        if(NetworkManager.IsInternetAvailable(HandleNetworkConnect) ==false)
            return;
        
        Dictionary<string, object> scoreAtDeathData = new Dictionary<string, object>()
        {
            {"scoreatdeath",_score}
        };

        AnalyticsService.Instance.CustomData("playerdeath", scoreAtDeathData);
        AnalyticsService.Instance.Flush();
    }
    private void HandleNetworkConnect(string _response)
    {

    }
}

