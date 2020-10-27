

using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDataConfiguration 
{
    public PlayerDataConfiguration(PlayerInput pi)
    {
        Input = pi;
        PlayerIndex = pi.playerIndex;

    }
    public Sprite PlayerShape { get; set; }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public UnityEngine.Color PlayerSpriteColor { get; set; }
}

public class NetworkedPlayerDataConfiguration
{

    public NetworkedPlayerDataConfiguration(Player _player)
    {
        thisPlayer = _player;
    }

    public Player thisPlayer { get; set; }
    public int NetworkedPlayerSpriteColor {get; set;}
    public bool isNetworkedPlayerReady {get; set;}

}
