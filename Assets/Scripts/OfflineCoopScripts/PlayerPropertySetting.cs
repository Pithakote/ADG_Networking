using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertySetting : MonoBehaviour
{
     public List<PlayerDataConfiguration> PlayerConfigs { get; set; }

    //for setting the color of the sprites/players
    //public void SetPlayerColor( int index, Image spriteColor)
    //{
    //    Debug.Log("Player: " + (index + 1) + " " + PlayerConfigs[index].IsReady);
    //    PlayerConfigs[index].PlayerSpriteColor = spriteColor.color;
    //}
    //public void SetShape(int index, Sprite PlayerShape)
    //{
    //    PlayerConfigs[index].PlayerShape = PlayerShape;
    //}
}
