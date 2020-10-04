using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    float speed = 5;
    [SerializeField]
    int _playerIndex = 0;
    public Vector2 MovementInput{ get; set; }//property value set in PlayerController
    public int PlayerIndex { get { return _playerIndex; } }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(MovementInput.x, MovementInput.y)
                                        * speed
                                        * Time.deltaTime);    
    }
}
