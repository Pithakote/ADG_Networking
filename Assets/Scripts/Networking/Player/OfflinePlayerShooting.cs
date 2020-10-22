using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePlayerShooting : PlayerShooting
{
    protected override void Update()
    {
        if (_isActivated)
        {
            Debug.Log("Shooting");
            fireTimer += Time.deltaTime;
            if (fireTimer >= _refireRate)
            {
                fireTimer = 0;


                Fire();

                Debug.Log("Offline shooting");

            }
        }
    }
}
