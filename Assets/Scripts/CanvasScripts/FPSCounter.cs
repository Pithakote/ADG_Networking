using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;


public class FPSCounter : MonoBehaviour
{
    
    [SerializeField] TMP_Text fpsText;
    public int avgFrameRate;
   

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        fpsText.text = avgFrameRate.ToString() + " FPS";
    }
}
