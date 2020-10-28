using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    float speed = 5;
    public bool _isPlayerAlive { get; set; }
    public Vector2 MovementInput { get; set; }//property value set in PlayerController
                                              //   public int PlayerIndex { get { return _playerIndex; } }

    public Vector3 _mousePos;
    [SerializeField] float offset;
    Rigidbody2D rb;
    // Update is called once per frame
    PlayerBase _player;
    private void Start()
    {
        _isPlayerAlive = true;
        _player = GetComponent<NetworkedPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {

        if (_isPlayerAlive)
        {
            rb.MovePosition(rb.position + MovementInput * speed * Time.deltaTime);




            MouseRotation();
        }
        

    }

    private void MouseRotation()
    {
        var lookDir = _mousePos ;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offset;
        rb.rotation = angle;

        if (PhotonNetwork.OfflineMode == false && !photonView.IsMine)
        {
            rb.rotation = _player.networkedRotation + Time.deltaTime * 100.0f;
        }
    }

    

}
