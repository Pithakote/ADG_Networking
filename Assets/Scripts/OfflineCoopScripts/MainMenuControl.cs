using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerSelectionScreen,
                MainMenuCanvas,
                PlayerConfigurationManager,
                PlayButton;
    [SerializeField] bool isMuted;
    [SerializeField] TMP_Text _muteButtonText;
    [SerializeField] Sprite _unmuteSprite, _muteSprite;
    [SerializeField] Button _muteButton;
    private void Start()
    {
        isMuted = MusicHandler.Instance._audioSource.mute;
        if (_muteButton)
        {
            ChangeButtonSprite(_muteButton);
        }


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

    public void MuteOrUnmute(Button _muteButton)
    {
        if (this._muteButton == null)
            this._muteButton = _muteButton;

        isMuted = !isMuted;
        MusicHandler.Instance._audioSource.mute = isMuted;
        ChangeButtonSprite(_muteButton);
    }

    private void ChangeButtonSprite(Button _muteButton)
    {
        if (isMuted)
        {
            _muteButton.GetComponent<Image>().sprite = _muteSprite;
            _muteButtonText.text = "Muted";
        }
        else
        {

            _muteButton.GetComponent<Image>().sprite = _unmuteSprite;
            _muteButtonText.text = "Unmuted";
        }
    }
}
