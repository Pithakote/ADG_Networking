using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

//--Ensures the following components are there in the player gameobject that inherit from this class
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(DontGoOffScreen))]

//------------------------------------------
public abstract class PlayerBase : MonoBehaviourPunCallbacks, ITakeDamage
{

    [SerializeField] protected Image _healthBar;
    [SerializeField] protected Sprite _deadIcon;

    public static GameObject LocalPlayerInstance;


    
    public bool HealthReduced { get; set; }
    
    
    protected SpriteRenderer _playerRenderer;
    public PlayerInput PlayerInput { get; set; }


    [SerializeField] protected float PlayerHealth;
    [SerializeField] protected float PlayerMaxHealth;
    public float networkedRotation { get; set; }

    protected PlayerInputHandler _playerInputHandler;


    public bool _isPlayerAlive;

    //-------Abstract functions inherited and used by the child classes----------------
    #region Abstract Functions
    //wanted to create one function to handle player data initialization but thw way of handling data
    //was different so had to create two function for offline and online player classes
    protected abstract void InitializePlayer(int _playerColorNumber);
    public abstract void InitializePlayer(PlayerDataConfiguration pc);
    protected abstract void Update();
    //  public abstract void ReduceHealth(int PlayerTakeDamageAmount);

    #endregion

    //-----------------------------------------------------------------
    protected virtual void Awake()
    {

        _playerRenderer = GetComponent<SpriteRenderer>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();



    }
    //In both Offline and Online players, change in the health bar will occur only when ReduceHealth function 
    //is called. This is called when the bullet class checks for the ITakeDamage interface 
    public virtual void ReduceHealth(int PlayerTakeDamageAmount)
    {
        _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);

    }
    protected virtual void Start()
    {
        _isPlayerAlive = true;
        HealthReduced = false;
        if (PlayerMaxHealth == 0)
        PlayerMaxHealth = 50;
        // PlayerHealth = PlayerMaxHealth;

        PlayerHealth = PlayerMaxHealth;
    }

    //PhotonNetwork function for handling when PlayerLeaves the room
    //need to inherit from MonoBehaviourPunCallbacks to use this

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.LocalPlayer && photonView.IsMine && PhotonNetwork.OfflineMode == false)
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
        }
    }
}
