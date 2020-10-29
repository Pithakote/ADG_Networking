using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(DontGoOffScreen))]
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
    protected int PlayerTakeDamageAmount;
    public float networkedRotation { get; set; }

    protected PlayerInputHandler _playerInputHandler;
    


    // Start is called before the first frame update
    protected abstract void InitializePlayer(int _playerColorNumber);

    public abstract void InitializePlayer(PlayerDataConfiguration pc);

    public bool _isPlayerAlive;

    protected virtual void Awake()
    {

        _playerRenderer = GetComponent<SpriteRenderer>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();



    }
    protected virtual void Start()
    {
        _isPlayerAlive = true;
        HealthReduced = false;
        PlayerTakeDamageAmount = 2;
        if (PlayerMaxHealth == 0)
        PlayerMaxHealth = 50;
        // PlayerHealth = PlayerMaxHealth;

        PlayerHealth = PlayerMaxHealth;
    }

    protected virtual void Update()
    {
        _healthBar.fillAmount = (PlayerHealth / PlayerMaxHealth);

    }



    public abstract void ReduceHealth();
    

  
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer == PhotonNetwork.LocalPlayer && photonView.IsMine && PhotonNetwork.OfflineMode == false)
        {
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
         //   _myCustomProperty.Remove("PlayerIndexNumber");
        }
    }
}
