using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

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

/*
public class RetinaImage : MonoBehaviour
{
    public int id = -1;
    public string name = "";
    public int xSize = 0;
    public int ySize = 0;
    public string filetype = "";
    public bool official = false;
    public string uploaded = "";
    public string imgBase64 = "";

    public IEnumerator GetImage(int id)
    {
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
                            this.id = Convert.ToInt32(tokens[3]);
                            break;
                        case "name":
                            this.name = tokens[3];
                            break;
                        case "xSize":
                            this.xSize = Convert.ToInt32(tokens[3]);
                            break;
                        case "ySize":
                            this.ySize = Convert.ToInt32(tokens[3]);
                            break;
                        case "filetype":
                            this.filetype = tokens[3];
                            break;
                        case "official":
                            if (tokens[3] == "1")
                            {
                                this.official = true;
                            }
                            else
                            {
                                this.official = false;
                            }
                            break;
                        case "uploaded":
                            this.uploaded = tokens[3];
                            break;
                        case "image":
                            this.imgBase64 = tokens[3];
                            break;
                    }
                }
                Debug.Log("Retina Image object updated.");
            }
        }
    }

    public void ApplyImage(Renderer rend)
    {
        byte[] imageBytes = Convert.FromBase64String(this.imgBase64);
        Texture2D tex = new Texture2D(this.xSize, this.ySize);
        tex.LoadImage(imageBytes);
        rend.material.SetTexture("_MainTex", tex);
        Debug.Log("Updated Texture.");
    }

    public void SaveToFile()
    {
        string destinationFolder = Application.persistentDataPath + "/retinaImages/";
        string destination = destinationFolder + this.id.ToString() + ".dat";
        FileStream file;

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        RetinaImageSerializable retinaImageSerializable = new RetinaImageSerializable(this);
        bf.Serialize(file, retinaImageSerializable);
        file.Close();
    }

    public IEnumerator LoadImage(int id)
    {
        Debug.Log("Load Image Called");
        string destinationFolder = Application.persistentDataPath + "/retinaImages/";
        string destination = destinationFolder + id.ToString() + ".dat";
        Debug.Log(destination);
        FileStream file;

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        Debug.Log("Checking if file exists");
        if (File.Exists(destination))
        {
            Debug.Log("Loaded from file");
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            RetinaImageSerializable tempRetinaImage = (RetinaImageSerializable)bf.Deserialize(file);
            this.id = tempRetinaImage.id;
            this.name = tempRetinaImage.name;
            this.xSize = tempRetinaImage.xSize;
            this.ySize = tempRetinaImage.ySize;
            this.filetype = tempRetinaImage.filetype;
            this.official = tempRetinaImage.official;
            this.uploaded = tempRetinaImage.uploaded;
            this.imgBase64 = tempRetinaImage.imgBase64;
            file.Close();
        }
        else
        {
            Debug.Log("Downloaded from webserver");
            StartCoroutine(this.GetImage(id));
            while (this.id == -1)
                yield return new WaitForSeconds(0.1f);
            this.SaveToFile();
            Debug.Log("Saving to File");
        }
    }
}*/

public class GetImageScript : MonoBehaviour
{
    private Renderer rend;
    public RetinaImage retinaImage;
    private bool notLoaded;
    public int imageId = 4;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        retinaImage = new RetinaImage();
        //StartCoroutine(GetImage(imageId, retinaImage));
        notLoaded = true;
        StartCoroutine(LoadImage(retinaImage, imageId));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(retinaImage.name);
        if (notLoaded && retinaImage.id != -1)
        {
            Debug.Log("Applied.");
            notLoaded = false;
            ApplyImage(retinaImage, rend);
        }
        
    }

    public IEnumerator GetImage(RetinaImage retinaImage, int id)
    {
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
                Debug.Log("Retina Image object updated.");
            }
        }
    }

    public void ApplyImage(RetinaImage retinaImage, Renderer rend)
    {
        byte[] imageBytes = Convert.FromBase64String(retinaImage.imgBase64);
        Texture2D tex = new Texture2D(retinaImage.xSize, retinaImage.ySize);
        tex.LoadImage(imageBytes);
        rend.material.SetTexture("_MainTex", tex);
        Debug.Log("Updated Texture.");
    }

    public void SaveToFile(RetinaImage retinaImage)
    {
        string destinationFolder = Application.persistentDataPath + "/retinaImages/";
        string destination = destinationFolder + retinaImage.id.ToString() + ".dat";
        FileStream file;

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, retinaImage);
        file.Close();
    }

    public IEnumerator LoadImage(RetinaImage retinaImage, int id)
    {
        Debug.Log("Load Image Called");
        string destinationFolder = Application.persistentDataPath + "/retinaImages/";
        string destination = destinationFolder + id.ToString() + ".dat";
        Debug.Log(destination);
        FileStream file;

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        Debug.Log("Checking if file exists");
        if (File.Exists(destination))
        {
            Debug.Log("Loaded from file");
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            RetinaImage tempRetinaImage = (RetinaImage)bf.Deserialize(file);
            retinaImage.id = tempRetinaImage.id;
            retinaImage.name = tempRetinaImage.name;
            retinaImage.xSize = tempRetinaImage.xSize;
            retinaImage.ySize = tempRetinaImage.ySize;
            retinaImage.filetype = tempRetinaImage.filetype;
            retinaImage.official = tempRetinaImage.official;
            retinaImage.uploaded = tempRetinaImage.uploaded;
            retinaImage.imgBase64 = tempRetinaImage.imgBase64;
            file.Close();
        }
        else
        {
            Debug.Log("Downloaded from webserver");
            StartCoroutine(GetImage(retinaImage, id));
            while (retinaImage.id == -1)
                yield return new WaitForSeconds(0.1f);
            SaveToFile(retinaImage);
            Debug.Log("Saving to File");
        }
    }
};
