using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
[CreateAssetMenu(fileName = "ScenesAndCanvases", menuName = "ScriptableObject/CanvasAndScene")]

public class CanvasSceneandCanvasHolder : ScriptableObject
{

    // public Dictionary<Scene, GameObject> ScenesAndCanvasesForScenes { get {return _scenesAndCanvasesForScenes; } }

    public ScenesWithTheirCanvases[] _scenesAndCanvasesArray;
}
