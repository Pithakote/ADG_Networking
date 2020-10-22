using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform bulletFirer;
    [SerializeField] GameObject _bullets;
    [SerializeField] float _refireRate = 0.2f;
    float fireTimer = 0;
    public bool _isActivated { get; set; }
    public SpriteRenderer _spriteRendererComponent{ get; set; }

    
    private void Awake()
    {

        _isActivated = false;
    }

    private void Update()
    {
        if (_isActivated)
        {
            Debug.Log("Shooting");
            fireTimer += Time.deltaTime;
            if (fireTimer >= _refireRate)
            {
                fireTimer = 0;
              if(PhotonNetwork.IsConnected)
                photonView.RPC("Fire", null);
              else
                Fire();
            }
        }
    }

    [PunRPC]
    void Fire()
    {
        var shot = Instantiate(_bullets,
                                            bulletFirer.position,
                                            bulletFirer.rotation
                                            );
    }
    
}
