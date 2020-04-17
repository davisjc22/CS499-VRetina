using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class RetinaImage
{
    public int id = -1;
    public string name = "";
    public int xSize = 0;
    public int ySize = 0;
    public string filetype = "";
    public bool official = false;
    public string uploaded = "";
    public string imgBase64 = "";
}

public class getImages : MonoBehaviour
{
    public GameObject buttonholder;
    public GameObject button;
    public GameObject updateLoading;
    private string folder;
    private int totalImages = 0;
    private int runs = 0;
    private bool updating = true;

    private void Start()
    {
        totalImages = 8;
        folder = Application.persistentDataPath + "/retinaImages";

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        StartCoroutine(findImages());
        updateFolder();
    }

    private void updateFolder()
    {
        for (int image = 0; image < totalImages; image++)
        {
            string filepath = folder + "/" + image.ToString() + ".dat";
            if (!File.Exists(filepath))
            {
                runs++;
                StartCoroutine(GetImage(image, filepath));           
            }
        }

        updating = false;

        if (runs == 0)
        {
            updateLoading.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!updating)
        {
            if (runs == 0)
            {
                updateLoading.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator getNumImages()
    {
        WWWForm form = new WWWForm();
        form.AddField("requestType", "count");
        using (UnityWebRequest www = UnityWebRequest.Post("http://ec2-13-58-160-235.us-east-2.compute.amazonaws.com/request.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                runs--;
            }
            else
            {
                Debug.Log("request complete!");
                string phrase = www.downloadHandler.text;
            }
        }
    }

    public IEnumerator GetImage(int id, string dest)
    {
        RetinaImage retinaImage = new RetinaImage();
        WWWForm form = new WWWForm();

        form.AddField("id", id);
        form.AddField("clientType", "app");

        using (UnityWebRequest www = UnityWebRequest.Post("http://ec2-13-58-160-235.us-east-2.compute.amazonaws.com/request.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("request complete!");

                string phrase = www.downloadHandler.text;
                string[] words = phrase.Split(',');

                foreach (var word in words)
                {
                    string[] tokens = word.Split('"');
                    switch (tokens[1])
                    {
                        case "id":
                            retinaImage.id = Convert.ToInt32(tokens[3]);
                            break;
                        case "name":
                            retinaImage.name = tokens[3];
                            break;
                        case "xSize":
                            retinaImage.xSize = Convert.ToInt32(tokens[3]);
                            break;
                        case "ySize":
                            retinaImage.ySize = Convert.ToInt32(tokens[3]);
                            break;
                        case "filetype":
                            retinaImage.filetype = tokens[3];
                            break;
                        case "official":
                            if (tokens[3] == "1")
                            {
                                retinaImage.official = true;
                            }
                            else
                            {
                                retinaImage.official = false;
                            }
                            break;
                        case "uploaded":
                            retinaImage.uploaded = tokens[3];
                            break;
                        case "image":
                            retinaImage.imgBase64 = tokens[3];
                            break;
                    }
                }
                SaveToFile(retinaImage, dest);
                runs--;
            }
        }
    }

    public void SaveToFile(RetinaImage retinaImage, string dest)
    {
        FileStream file;

        file = File.Create(dest);
      
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, retinaImage);
        file.Close();

        StartCoroutine(makeButton(retinaImage));
    }

    private IEnumerator makeButton(RetinaImage retinaImage)
    {
        byte[] imageBytes = Convert.FromBase64String(retinaImage.imgBase64);
        Texture2D texture = new Texture2D(retinaImage.xSize, retinaImage.ySize);
        yield return texture.LoadImage(imageBytes);

        GameObject newButton = Instantiate(button) as GameObject;
        newButton.transform.SetParent(buttonholder.transform, false);
        newButton.GetComponent<RawImage>().texture = texture;
        newButton.GetComponent<changeImage>().texture = texture;
    }

    private IEnumerator findImages()
    {
        DirectoryInfo dir = new DirectoryInfo(folder);

        FileInfo[] info = dir.GetFiles();
        FileStream f;

        foreach (FileInfo file in info)
        {
            RetinaImage retinaImage = new RetinaImage();

            if (file.Extension.Contains("dat"))
            {
                yield return f = File.OpenRead(file.FullName);
                BinaryFormatter bf = new BinaryFormatter();
                RetinaImage tempRetinaImage = (RetinaImage)bf.Deserialize(f);
                retinaImage.id = tempRetinaImage.id;
                retinaImage.name = tempRetinaImage.name;
                retinaImage.xSize = tempRetinaImage.xSize;
                retinaImage.ySize = tempRetinaImage.ySize;
                retinaImage.filetype = tempRetinaImage.filetype;
                retinaImage.official = tempRetinaImage.official;
                retinaImage.uploaded = tempRetinaImage.uploaded;
                retinaImage.imgBase64 = tempRetinaImage.imgBase64;
                f.Close();

                StartCoroutine(makeButton(retinaImage));
            }
        }
    }
}
