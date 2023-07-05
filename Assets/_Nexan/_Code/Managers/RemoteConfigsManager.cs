using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfigsManager : SingletonParent<RemoteConfigsManager>
{
    public float retrievedHealthDrop;
    public float retrievedPickupSpawnRate;
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
        retrievedPickupSpawnRate = runtimeConfig.GetFloat("pickupspawnrate");
        GameManager.Instance.SetHealthDrop(retrievedHealthDrop);
        GameManager.Instance.SetPickupSpawnRate(retrievedPickupSpawnRate);
    }
}