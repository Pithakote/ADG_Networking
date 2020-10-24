using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    [SerializeField] protected Transform bulletFirer;
    [SerializeField] protected GameObject _bullets;
    [SerializeField] protected float _refireRate = 0.2f;
    protected float fireTimer = 0;
    bool isShooting;
    public bool _isActivated { get { return isShooting; } set { isShooting = value; } }
    public SpriteRenderer _spriteRendererComponent{ get; set; }

    
    private void Awake()
    {

        _isActivated = false;
    }

    protected  virtual void Update()
    {
        if (_isActivated)
        {
            Debug.Log("Shooting");
            fireTimer += Time.deltaTime;
            if (fireTimer >= _refireRate)
            {
                fireTimer = 0;

               
                    Fire();
              
            }
        }
    }

    [PunRPC]
    protected virtual void Fire()
    {
        var shot = Instantiate(_bullets,
                                            bulletFirer.position,
                                            bulletFirer.rotation
                                            );
    }
    
}
