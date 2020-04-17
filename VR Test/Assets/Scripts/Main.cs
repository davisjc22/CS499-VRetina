using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Main : MonoBehaviour
{
    //private static bool isAR;
    //private static string imageName;

    // // Start is called before the first frame update
    // void Start()
    // {
    //
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //
    // }

    public static bool isAR
    {
        get
        {
            return isAR;
        }
        set
        {
            isAR = value;
        }
    }

    public static string imageName
    {
        get
        {
            return imageName;
        }
        set
        {
            imageName = value;
        }
    }

    public void LoadVR() //Loads the VR mode of the app
    {
        SceneManager.LoadScene("VRDemo");
    }

    public void LoadAR() //Loads the AR mode of the app
    {
        SceneManager.LoadScene("ARDemo");
    }


}
