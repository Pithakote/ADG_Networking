using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkedPlayerShooting : PlayerShooting
{

   
    protected override void Update()
    {
        //if the player is local and the player is shooting.
        //value is determined by the PlayerInputHandler class
        if (photonView.IsMine && _isActivated)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= _refireRate)
            {
                fireTimer = 0;
                Fire();
             

            }

            
        }
    }
   // [PunRPC]
    protected override void Fire()
    {
        if (_useObjectPool)
        {


            GameObject _bulletShot = ObjectPool.Instance.Get();

            _bulletShot.transform.position = bulletFirer.position;
            _bulletShot.transform.rotation = bulletFirer.rotation;

            _bulletShot.SetActive(true);
        }
        else
        {
             PhotonNetwork.Instantiate(_bullets.name,
                                               bulletFirer.position,
                                               bulletFirer.rotation,
                                               0);
        }
    }

}
