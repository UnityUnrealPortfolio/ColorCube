using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfigsManager : SingletonParent<RemoteConfigsManager>
{
    public float retrievedHealthDrop;
    public struct UserAttributes
    {

    }
    public struct AppAttributes
    {

    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    async void Start()
    {
        await UnityServices.InitializeAsync();

        RuntimeConfig runtimeConfig = await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());

        retrievedHealthDrop = runtimeConfig.GetFloat("healthdrop");
        GameManager.Instance.SetHealthDrop(retrievedHealthDrop);
    }
}
