using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerSelectionScreen,
                MainMenuCanvas,
                PlayerConfigurationManager,
                PlayButton;
    bool isMuted;
    [SerializeField] TMP_Text _muteButtonText;
    private void Start()
    {
        isMuted = false;
        if (PlayButton == null)
            return;

        if (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer
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

    public void MuteOrUnmute()
    {
       
            isMuted = !isMuted;
            MusicHandler.Instance._audioSource.mute = isMuted;

        if (isMuted)
        {
            _muteButtonText.text = "Muted";
        }
        else
        {
            _muteButtonText.text = "Unmuted";
        }
    }
}
