using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerSelectionScreen,
                MainMenuCanvas,
                PlayerConfigurationManager,
                PlayButton;
    private void Start()
    {
        if (PlayButton != null &&
            (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer
            )
            )
        {
            PlayButton.SetActive(false);
        }
        else
        {
            PlayButton.SetActive(true);
        }
    }
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
        SceneManager.LoadScene("NetworkedSelectionScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
