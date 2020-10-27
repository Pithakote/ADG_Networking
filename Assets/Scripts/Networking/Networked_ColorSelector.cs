using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Networked_ColorSelector : MonoBehaviour
{
    [SerializeField] TMP_Text _playerNameDisplayText;
    [SerializeField] Button[] _buttonsInPanel;



    private void Awake()
    {
        _buttonsInPanel = transform.GetComponentsInChildren<Button>();
    }

    public void SetupNetworkedPanel(string _playerName)
    {
        _playerNameDisplayText.text = _playerName;
    }
}
