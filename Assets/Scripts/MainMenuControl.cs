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
        PlayerSelectionScreen.SetActive(true);
        Instantiate(PlayerConfigurationManager);
        MainMenuCanvas.SetActive(false);
    }

    public void GoBack()
    {
      //  PlayerConfigurationManager.Ins
     
        SceneManager.LoadScene("PlayerSetup");
    //    CanvasManager.Instance.ChangeCanvasForScene();
    }
}
