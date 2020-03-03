using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    void Start()
    {
        //StartCoroutine(LoadDevice(""));
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
