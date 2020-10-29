using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    [SerializeField] protected Transform bulletFirer;
    [SerializeField] protected GameObject _bullets;
    [SerializeField] protected float _refireRate = 0.2f;
    [SerializeField] protected bool _useObjectPool;
    [SerializeField] protected float _numberOfBulletsInPool;

    protected float fireTimer = 0;
    bool isShooting;
    public bool _isActivated { get { return isShooting; } set { isShooting = value; } }
    public SpriteRenderer _spriteRendererComponent{ get; set; }

    
    private void Awake()
    {
        _useObjectPool = false;
        _isActivated = false;
    }

    private void Start()
    {
        if (_numberOfBulletsInPool == 0)
            _numberOfBulletsInPool = 20;

    
    }

    protected  virtual void Update()
    {
       
    }

    
    protected virtual void Fire()
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
             Instantiate(_bullets,
                                               bulletFirer.position,
                                               bulletFirer.rotation
                                               );
        }

    }
    
}
