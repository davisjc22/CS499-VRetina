using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class changeImage : MonoBehaviour
{
    public Texture2D texture;

    public void updateImage()
    {
        GameObject view = GameObject.Find("Scroll View");
        view.gameObject.SetActive(false);

        GameObject eyeball = GameObject.Find("EyeBall");
        eyeball.GetComponent<Renderer>().material.mainTexture = texture;
    }
}

