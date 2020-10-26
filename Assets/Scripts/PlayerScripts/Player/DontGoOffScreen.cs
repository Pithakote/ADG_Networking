using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoOffScreen : MonoBehaviour
{
    Vector2 _screenBounds;
    float _playerWidth, _playerHeight;
    SpriteRenderer _playerSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        

    }
    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
                                                                    Screen.width,
                                                                    Screen.height,
                                                                    Camera.main.transform.position.z
                                                                    )
                                                                    );
        _playerWidth = _playerSprite.bounds.extents.x/2;
        _playerHeight = _playerSprite.bounds.extents.y/2;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x,
                                _screenBounds.x * -1 + _playerWidth,
                                _screenBounds.x  - _playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y,
                                _screenBounds.y * -1 + _playerHeight,
                                _screenBounds.y  - _playerHeight);

        transform.position = viewPos;
    }
}
