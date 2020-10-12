using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("System Events")]
    public UnityEvent onSwitchedScreen = new UnityEvent();
    [SerializeField] CanvasController _startScreen;
    public List<CanvasController> _canvasControllerList;
    [SerializeField]
     CanvasController _currentScene,_previousScreen;
    [SerializeField] GameObject _playerConfigManager;
    public CanvasController CurrentScene { get { return _currentScene; } }
    public CanvasController PreviousScene { get { return _previousScreen; } }
    //set { _currentScene = value; } }

    public static CanvasManager Instance { get; private set; }
    private void Awake()
    {
        _canvasControllerList = GetComponentsInChildren<CanvasController>(true).ToList();
     //  _canvasControllerList.ForEach(cs => cs.gameObject.SetActive(true));

        if (Instance == null)
        {
            Instance = this;
            //  DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        if (_startScreen)
            SwitchScreen(_startScreen);
    }
    public void SwitchScreen(CanvasController aScreen)
    {
        if (aScreen)
        {
            if (_currentScene)
            {
                _currentScene.gameObject.SetActive(false);
                _previousScreen = _currentScene;
            }

            _currentScene = aScreen;
            _currentScene.gameObject.SetActive(true);
            _currentScene.StartScreen();

            if (onSwitchedScreen != null)
            {
                onSwitchedScreen.Invoke();
            }
        }
    }

    public void GoToPreviousScreen()
    {
        if (_previousScreen)
             SwitchScreen(_previousScreen);
    }

    public void InitializePlayerManager()
    {
        if(!GameObject.Find("PlayerConfigurationManager(Clone)"))
        Instantiate(_playerConfigManager);
    }
    /*
    [SerializeField]
    List<CanvasController> _canvasControllerList;
    public static CanvasManager Instance { get; private set; }
    [SerializeField]
    CanvasController _previousActiveCanvas;
    [SerializeField]
    CanvasController _currentCanvas;
    public CanvasController _newCanvas { get {return _currentCanvas; } set { _currentCanvas = value; } }
    [SerializeField]
    CanvasController _lastActiveCanvas;

    [SerializeField] GameObject _configManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          //  DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }

        

        _canvasControllerList = GetComponentsInChildren<CanvasController>(true).ToList();
        _canvasControllerList.ForEach(canvas => canvas.gameObject.SetActive(false));
        
        //_newCanvas.CanvasType = CanvasTypesInsideScenes.MainMenu;
         SwitchCanvas(CanvasTypesInsideScenes.MainMenu);

    }
    private void Start()
    {
        _currentCanvas.CanvasType = CanvasTypesInsideScenes.MainMenu;
    }
    public void SwitchCanvas(CanvasTypesInsideScenes _cType)
    {
        if (_currentCanvas != null &&
            _currentCanvas.CanvasType != _cType)
        {
            _previousActiveCanvas = _newCanvas;
         //   _previousActiveCanvas.gameObject.SetActive(false);
        }
        if (_lastActiveCanvas)
            _lastActiveCanvas.gameObject.SetActive(false);
        _currentCanvas = _canvasControllerList.Find(
                                        newCanvas => newCanvas.CanvasType == _cType);
        if (_currentCanvas != null)
        {
            _currentCanvas.gameObject.SetActive(true);
            _lastActiveCanvas = _newCanvas;
          //  _previousActiveCanvas = _newCanvas;
        }

        if (_currentCanvas.CanvasType == CanvasTypesInsideScenes.LocalPlay)
        {
            
            Instantiate(_configManager);
        }
    }
    public void PreviousCanvas()
    {
        if (_previousActiveCanvas)
            return;

        SwitchCanvas(_previousActiveCanvas.CanvasType);
    }
    */
}
#region OldCode
/* [SerializeField] CanvasSceneandCanvasHolder _sceneAndCanvases;
    //  public Dictionary<Scene, GameObject> _scenesAndCanvasesForScenes;
    string CurrentSceneName;
    public static CanvasManager Instance { get; private set; }

    GameObject currentCanvasForScene, previousCanvasForScene;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangeCanvasForScene;
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }


        AssignCorrectSceneCanvas(SceneManager.GetActiveScene());

    }


    

    public void ChangeCanvasForScene(Scene PreviousScene, Scene NewScene)
    {
        if (NewScene.name != CurrentSceneName)
            AssignCorrectSceneCanvas(NewScene);

    }

    void AssignCorrectSceneCanvas(Scene scene)
    {
        if (currentCanvasForScene)
        {
            previousCanvasForScene = currentCanvasForScene;
            previousCanvasForScene.SetActive(false);
        }
        foreach (var sceneAsset in _sceneAndCanvases._scenesAndCanvasesArray)
        {
            if (sceneAsset.TheSceneName.name == scene.name)
            {
                GameObject _childCanvas;
                if (GameObject.Find(sceneAsset.CanvasesForScenes.name))
                {
                    _childCanvas = GameObject.Find(sceneAsset.CanvasesForScenes.name);
                    if (_childCanvas != null && !_childCanvas.activeSelf)
                    {

                        _childCanvas.SetActive(true);
                    }
                }
                else
                {
                    _childCanvas = Instantiate(sceneAsset.CanvasesForScenes);
                    _childCanvas.name = sceneAsset.CanvasesForScenes.name;
                    _childCanvas.transform.SetParent(transform);
                }
                currentCanvasForScene = _childCanvas;
                CurrentSceneName = scene.name;
            }
        }


    }*/
#endregion