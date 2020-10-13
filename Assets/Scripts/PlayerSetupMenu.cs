using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenu : MonoBehaviour
{
    int _playerIndex;

    [Header("UI GameObjects")]
    [SerializeField]
    TextMeshProUGUI _titleText;
    [SerializeField]
    GameObject _readyPanel, _menuPanel;
    [SerializeField]
    Button _readyButton;

    float _ignoreInputTime = 1.5f;
    bool _inputEnabled;

    [SerializeField]
    public Button[] _buttonsInPanel;

    Button _pressedButton;

    [SerializeField] PlayerColorAndShape _playerColorAndShape;
    string _buttonName;
    public PlayerColorAndShape PlayerColorAndShape { get { return _playerColorAndShape; } }
 
    private void Awake()
    {
        _buttonsInPanel = GetComponentsInChildren<Button>();

        PlayerConfigurationManager.Instance.ButtonSelectEvent += DisableButton;

    }
    void DisableButton(Button _btn)
    {
        foreach (var button in _buttonsInPanel.Where(btnarr => btnarr.name == _btn.name))
            button.interactable = false;

    }

    private void OnDisable()
    {
        PlayerConfigurationManager.Instance.ButtonSelectEvent -= DisableButton;

    }
    public void SetPlayerIndex(int pi)
    {
        _playerIndex = pi;
        _titleText.SetText("Player "+(pi+1).ToString());
        _ignoreInputTime = Time.time + _ignoreInputTime;
    }
    
    void Update()
    {
        if (Time.time > _ignoreInputTime)
            _inputEnabled = true;

    }
  
    public void SetColor(Image color)
    {
        if (!_inputEnabled)
            return;

        PlayerConfigurationManager.Instance.SetShape(_playerIndex,_playerColorAndShape._playerShape[_playerIndex]);
        PlayerConfigurationManager.Instance.SetPlayerColor(_playerIndex, color);


        _readyPanel.SetActive(true);
        _readyButton.Select();
        _menuPanel.SetActive(false);

        PlayerConfigurationManager.Instance.ButtonEventRaiser(_pressedButton);

    }
    public void AssignButtonName(Button _assignedButton)
    {
        _buttonName = _assignedButton.name;
        _pressedButton = _assignedButton;
    }

    //public void SetShape(Image shape)
    //{
    //    if (!_inputEnabled)
    //        return;

    //    PlayerConfigurationManager.Instance.SetPlayerShape(_playerIndex, shape.sprite);

    //  //  _assignedButton.interactable = false;







    // //   _playerColorAndShape._playerShape[_playerIndex].IsSelected = true;
    //    _readyPanel.SetActive(true);
    //    _readyButton.Select();
    //    _menuPanel.SetActive(false);
    //    Debug.Log(_buttonName);

    //    /* 
    //     * foreach (var eachInstanceOfMenu in PlayerConfigurationManager.Instance.ListOfMenuUI.Where(
    //                  eim => eim.name == _buttonName))
    //          if (eachInstanceOfMenu.GetComponent<PlayerSetupMenu>()._buttonsInPanel.Any(bn => bn.name == _buttonName ?  bn.interactable : false) ;
    //          */

    //    PlayerConfigurationManager.Instance.ButtonEventRaiser(_pressedButton);



    //}
    public void ReadyPlayer()
    {
        if (!_inputEnabled)
            return;

        PlayerConfigurationManager.Instance.ReadyPlayer(_playerIndex);
        _readyButton.gameObject.SetActive(false);
    }
    /*
     * 
    

    private void OnDisable()
    {
       // PlayerConfigurationManager.Instance.ButtonSelectEvent -= DisableButton;

    }
    private void Start()
    {

        //int _playerShapeLength = _buttonsInPanel.Length;

        //   foreach (Button button in _buttonsInPanel)
        //   button.targetGraphic = _playerColorAndShape[button];

     //   if (_playerColorAndShape._playerShape.Length != _buttonsInPanel.Length)
      //      return;

     //   for (int i = 0; i<_buttonsInPanel.Length; i++)
     //       _buttonsInPanel[i].image.sprite = _playerColorAndShape._playerShape[i]._playerShape;

        //_buttonsInPanel.All(bttn=>bttn.sprite = _playerColorAndShape._playerShape);
    }
    public void AssignButtonName(Button _assignedButton)
    {
        _buttonName = _assignedButton.name;
        _pressedButton = _assignedButton;
    }
    void DisableButton(Button _btn)
    {
        foreach (var button in _buttonsInPanel.Where(btnarr => btnarr.name == _btn.name))
            button.interactable = false;

    }*/
}
