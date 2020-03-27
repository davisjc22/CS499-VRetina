using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeImage : MonoBehaviour
{
    public void getImage()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                Texture texture = NativeGallery.LoadImageAtPath(path, 512);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                GameObject eyeball = GameObject.Find("EyeBall");
                eyeball.GetComponent<Renderer>().material.mainTexture = texture;
            }
        }, "Select an image", "image/png");

        Debug.Log("Permission result: " + permission);
    }
}

