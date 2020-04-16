using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openImageSelection : MonoBehaviour
{
    public GameObject Scrollview;

    public void openMenu()
    {
        Scrollview.gameObject.SetActive(true);
    }

    public void closeMenu()
    {
        Scrollview.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                closeMenu();
            }
        }
    }
}
