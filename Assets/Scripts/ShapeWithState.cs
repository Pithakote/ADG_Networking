using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShapeWithState 
{
    public Sprite _playerShape;

    public bool IsSelected = false;

    public string GetName { get; set; }
}
