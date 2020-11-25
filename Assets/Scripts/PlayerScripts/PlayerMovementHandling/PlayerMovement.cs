using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    float speed = 5;
    public bool _isPlayerAlive { get; set; }

    //property value set in PlayerInputhandler
    public Vector2 MovementInput { get; set; }
                                            
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

    //all movement code is being used in FixedUpdate
    void FixedUpdate()
    {
        //rigidbody us being used to control the translation and rotation code

        //only use this movement and rotation code if the player is alive
        if (_isPlayerAlive)
        {

            //using the MovementInput value being set from the PlayerInputHandler class 
            rb.MovePosition(rb.position + MovementInput * speed * Time.deltaTime);




            //Rotationcode
            MouseRotation();
        }
        

    }

    private void MouseRotation()
    {

        //lookDir is being set through the mouseOis variable in PlayerInputHandler class
        var lookDir = _mousePos ;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - offset;
        rb.rotation = angle;


        //only use this code if it is an online mode and the player is the local player
        if (PhotonNetwork.OfflineMode == false && !photonView.IsMine)
        {
            //networkredRotation is being observed so that it is synced in all other clients 
            rb.rotation = _player.networkedRotation + Time.deltaTime * 100.0f;
        }
    }

    

}
