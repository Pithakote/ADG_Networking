using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;


public class FPSCounter : MonoBehaviour
{
    public float Rate = 50.0f;
    float currentFrameTime;
    [SerializeField] TMP_Text fpsText;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
       
    }

    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / Rate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;

            fpsText.text = currentFrameTime + " FPS";
        }
        
    }
}
