using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleServerDropdown : MonoBehaviourPunCallbacks
{
    
    public void HandleServerOptions(int val)
    {
        var serverLocation = PhotonNetwork.PhotonServerSettings.AppSettings;
        if (val == 0)
        {
            serverLocation.FixedRegion = "eu";
        }
        if (val == 1)
        {

            serverLocation.FixedRegion = "au";
        }
        if (val == 2)
        {

            serverLocation.FixedRegion = "asia";
        }
        if (val == 3)
        {

            serverLocation.FixedRegion = "us";
        }

        if (val == 3)
        {

            serverLocation.FixedRegion = "usw";
        }
    }
}
