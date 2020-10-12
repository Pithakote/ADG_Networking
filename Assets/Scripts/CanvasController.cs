using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Selectable m_StartSelectable;
    public CanvasTypesInsideScenes CanvasType;

    public UnityEvent onScreenStart = new UnityEvent();
    public UnityEvent onScreenClose = new UnityEvent();
    
    private void Start()
    {
        if (!CanvasManager.Instance._canvasControllerList.Find(obj => obj == this))
            return;

        CanvasManager.Instance._canvasControllerList.Add(this);
    }
    public void StartScreen()
    {
        gameObject.SetActive(true);
        if (onScreenStart != null)
            onScreenStart.Invoke();

    }
    public void CloseScreen()
    {
        gameObject.SetActive(false);
        if (onScreenClose != null)
            onScreenClose.Invoke();
    }
}
