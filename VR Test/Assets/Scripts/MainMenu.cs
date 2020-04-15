using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadDevice(""));
    }

    public void LoadVR() //Loads the VR mode of the app
    {
        SceneManager.LoadScene("VRDemo");
    }

    public void LoadAR() //Loads the AR mode of the app
    {
        SceneManager.LoadScene("ARDemo");
    }

    IEnumerator LoadDevice(string newDevice)
    {
        if (String.Compare(XRSettings.loadedDeviceName, newDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            XRSettings.enabled = true;
        }

    }
}
