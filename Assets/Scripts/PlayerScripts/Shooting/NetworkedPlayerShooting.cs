using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkedPlayerShooting : PlayerShooting
{

   
    protected override void Update()
    {
        if (_isActivated)
        {
           // Debug.Log("Shooting");
            fireTimer += Time.deltaTime;
            if (fireTimer >= _refireRate)
            {
                fireTimer = 0;

               photonView.RPC("Fire",RpcTarget.All,  null);

             //   Debug.Log("Online shooting");

            }
        }
    }

    
}
