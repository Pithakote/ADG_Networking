using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool _isActivated { get; set; }
    public SpriteRenderer _spriteRendererComponent{ get; set; }
    private void Awake()
    {
        _isActivated = false;
    }

    private void Update()
    {
        if (_isActivated)
            Debug.Log("Shooting");
    }
    
}
