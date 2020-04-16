using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;

public class getImages : MonoBehaviour
{
    Texture2D[] textures = null;
    public GameObject buttonholder;
    public GameObject button;

    private bool loaded = false;

    private void Start()
    {
        StartCoroutine(findImages());
    }

    //public void images()
    //{
    //    if (!loaded)
    //    {
    //        loaded = true;  
    //        StartCoroutine(findImages());
    //    }
    //}

    IEnumerator findImages()
    {
        //Search for files
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/retinaImages/");
        string[] extensions = new[] { ".jpg", ".JPG", ".jpeg", ".JPEG", ".png", ".PNG", ".ogg", ".OGG" };
        FileInfo[] info = dir.GetFiles().Where(f => extensions.Contains(f.Extension.ToLower())).ToArray();

        //yield return info;

        for (int i = 0; i < info.Length; i++)
        {
            //GameObject newButton = Instantiate(button) as GameObject;
            //yield return newButton;
            //newButton.transform.SetParent(buttonholder.transform, false);
            //newButton.transform.GetChild(0).GetComponent<Text>().text = Path.GetFileName(info[i].FullName);

            byte[] bytes = File.ReadAllBytes(info[i].FullName);
            //yield return bytes;

            Texture2D texture = new Texture2D(2, 2);
            yield return texture.LoadImage(bytes);

            GameObject newButton = Instantiate(button) as GameObject;
            newButton.transform.SetParent(buttonholder.transform, false);
            newButton.GetComponent<RawImage>().texture = texture;
            newButton.GetComponent<changeImage>().texture = texture;

            //GameObject newButton = Instantiate(button) as GameObject;
            //newButton.transform.SetParent(buttonholder.transform, false);
            //newButton.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(1f, 1f), 100.0f);
            //newButton.GetComponent<Image>().SetNativeSize();
            //newButton.GetComponent<changeImage>().texture = texture;
            //yield return null;
        }
    }
}
