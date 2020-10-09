using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerSelectionScreen, MainMenuCanvas, PlayerConfigurationManager;

    public void OpenPlayerSelection()
    {
        PlayerSelectionScreen.SetActive(true);
        PlayerConfigurationManager.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }
}
