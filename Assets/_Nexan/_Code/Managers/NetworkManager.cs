using DeadMosquito.AndroidGoodies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class retrieves info about device connectivity
/// and stores it in a globally accessible variable
/// </summary>
public static class NetworkManager 
{
   public static bool IsInternetAvailable(Action<string> _callback)
    {
       if(AGNetwork.IsWifiConnected() || AGNetwork.IsMobileConnected())
        {
            _callback("Internet available");
            return true;
        }
        else
        {
            _callback("Internet unavailable");
            return false;
        }
    }
}
