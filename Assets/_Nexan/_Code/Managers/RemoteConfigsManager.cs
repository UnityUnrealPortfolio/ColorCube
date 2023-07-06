using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using UnityEngine;
using TMPro;

public class RemoteConfigsManager : SingletonParent<RemoteConfigsManager>
{
    public TMP_Text debugText;//ToDo:public for testing
    private float retrievedHealthDrop;
    private float retrievedPickupSpawnRate;

    #region Attribute Structs For GameOverrides
    public struct UserAttributes
    {

    }
    public struct AppAttributes
    {

    } 
    #endregion
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    async void Start()
    {
        await UnityServices.InitializeAsync();

        //First performs an internet check before fetching remote configs
        if(NetworkManager.IsInternetAvailable(HandleNetworkCheck))
        {

            RuntimeConfig runtimeConfig = await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());

            retrievedHealthDrop = runtimeConfig.GetFloat("healthdrop");
            retrievedPickupSpawnRate = runtimeConfig.GetFloat("pickupspawnrate");

            GameManager.Instance.SetHealthDrop(retrievedHealthDrop);
            GameManager.Instance.SetPickupSpawnRate(retrievedPickupSpawnRate);
        }

        
    }

    private void HandleNetworkCheck(string _response)
    {
        //debugText.text = _response;
    }
}
