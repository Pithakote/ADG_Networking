using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenu : MonoBehaviour
{
    int _playerIndex;

    [SerializeField]
    TextMeshProUGUI _titleText;

    [SerializeField]
    GameObject _readyPanel;
    [SerializeField]
    GameObject _menuPanel;
    [SerializeField]
    Button _readyButton;

    float _ignoreInputTime = 1.5f;
    bool _inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        

        _playerIndex = pi;
        _titleText.SetText("Player "+(pi+1).ToString());
        _ignoreInputTime = Time.time + _ignoreInputTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > _ignoreInputTime)
            _inputEnabled = true;


    }

    public void SetColor(Image color)
    {
        if (!_inputEnabled)
            return;

        PlayerConfigurationManager.Instance.SetPlayerColor(_playerIndex, color);
        _readyPanel.SetActive(true);
        _readyButton.Select();
        _menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!_inputEnabled)
            return;

        PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
        _readyButton.gameObject.SetActive(false);
    }
}
