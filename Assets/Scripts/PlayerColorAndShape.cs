using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShape&Color", menuName = "ScriptableObject/PlayerData") ]
public class PlayerColorAndShape : ScriptableObject
{
    public Color _playerColor;
    public Sprite [] _playerShape;
  
}
