using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public ButtonTypes _buttonType;
    public CanvasTypesInsideScenes _desiredCanvasType;
    CanvasManager _canvasManager;
    Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClick);
        _canvasManager = CanvasManager.Instance;
    }
    void OnButtonClick()
    {
       //    _canvasManager.SwitchScreen(_desiredCanvasType);
       
    }
    
}
