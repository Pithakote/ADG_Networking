using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerSelectionScreen, MainMenuCanvas, PlayerConfigurationManager;

    public void OpenPlayerSelection()
    {
        SceneManager.LoadScene("PlayerSelection");
    }

    public void GoBack()
    {
      //  PlayerConfigurationManager.Ins
     
        SceneManager.LoadScene("PlayerSetup");
    //    CanvasManager.Instance.ChangeCanvasForScene();
    }

    public void OpenOnlineMenu()
    {
        SceneManager.LoadScene("NetworkedScene");
    }
}
