using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    List<CanvasController> _canvasControllerList;
    public static CanvasManager Instance { get; private set; }
    [SerializeField]
    CanvasController _previousActiveCanvas;
    public CanvasController _newCanvas { get; set; }
    CanvasController _lastActiveCanvas;
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

        

        _canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        _canvasControllerList.ForEach(canvas => canvas.gameObject.SetActive(false));
        SwitchCanvas(CanvasTypesInsideScenes.MainMenu);
    }

    public void SwitchCanvas(CanvasTypesInsideScenes _cType)
    {
        if (_newCanvas != null &&
            _newCanvas.CanvasType != _cType)
        {
            _previousActiveCanvas = _newCanvas;
         //   _previousActiveCanvas.gameObject.SetActive(false);
        }
        if (_lastActiveCanvas)
            _lastActiveCanvas.gameObject.SetActive(false);
         _newCanvas = _canvasControllerList.Find(
                                        newCanvas => newCanvas.CanvasType == _cType);
        if (_newCanvas != null)
        {
            _newCanvas.gameObject.SetActive(true);
            _lastActiveCanvas = _newCanvas;
          //  _previousActiveCanvas = _newCanvas;
        }
    }
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