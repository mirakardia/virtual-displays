using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    public Camera myCamera;

    private bool takeScreenshot;
    private string screenshotFolder;

    public bool takeScreenshotWithDelay;
    public float screenshotDelay;

    public bool takeScreenshotWithInterval;
    public float screenshotInterval;

    // Start is called before the first frame update
    void Awake()
    {
        takeScreenshot = false;

        screenshotFolder = Application.persistentDataPath + "/Screenshots/";

        Debug.Log("Screenshot folder: " + screenshotFolder);

        if (takeScreenshotWithDelay)
        {
            Invoke("TakeScreenshot", screenshotDelay);
        }
        if (takeScreenshotWithInterval)
        {
            InvokeRepeating("TakeScreenshot", screenshotInterval, screenshotInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        try
        {
            // Check if directory exists.
            if (!Directory.Exists(screenshotFolder))
            {
                // If not, create it.
                Directory.CreateDirectory(screenshotFolder);
            }

            string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

            Debug.Log("Saving screenshot to " + screenshotFolder);

            ScreenCapture.CaptureScreenshot(screenshotFolder + fileName);
        }
        catch (Exception e)
        {
            Debug.Log("Could not capture screenshot: " + e.ToString());
        }
    }
}
