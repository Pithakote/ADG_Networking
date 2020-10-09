using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShape&Color", menuName = "ScriptableObject/PlayerData") ]
public class PlayerColorAndShape : ScriptableObject
{
    public Color _playerColor;
    public ShapeWithState[] _playerShape;
   // public bool IsTaken = false;
}
