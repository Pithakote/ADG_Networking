using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkedPlayerShooting : PlayerShooting
{

   
    protected override void Update()
    {
        if (photonView.IsMine && _isActivated)
        {
           // Debug.Log("Shooting");
            fireTimer += Time.deltaTime;
            if (fireTimer >= _refireRate)
            {
                fireTimer = 0;

               //   photonView.RPC("Fire",RpcTarget.All,  null);
                Fire();
                //   Debug.Log("Online shooting");
                //PhotonNetwork.Instantiate(_bullets.name,
                //                               bulletFirer.position,
                //                               bulletFirer.rotation,
                //                               0);

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
