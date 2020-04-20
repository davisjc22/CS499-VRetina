using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;

public class getImages : MonoBehaviour
{
    public GameObject buttonholder;
    public GameObject button;
    public GameObject updateLoading;
    private string listIds;
    private string folder;
    private int runs = 0;
    private bool updating = true;

    private void Start()
    {
        folder = Application.persistentDataPath + "/retinaImages";

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        StartCoroutine(findImages());
        StartCoroutine(updateFolder());
    }

    private IEnumerator updateFolder()
    {
        yield return StartCoroutine(getNumImages());

        int[] imageIds = Array.ConvertAll(listIds.Split(','), int.Parse);

        Debug.Log("Updating Folder.");
        for (int image = 0; image < imageIds.Length; image++)
        {
            string filepath = folder + "/" + imageIds[image].ToString() + ".dat";
            if (!File.Exists(filepath))
            {
                runs++;
                StartCoroutine(GetImage(imageIds[image], filepath));
            }
        }

        updating = false;

        if (runs == 0)
            updateLoading.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!updating)
        {
            if (runs == 0)
                updateLoading.gameObject.SetActive(false);
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
                listIds = phrase;
                Debug.Log(phrase);
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
                Debug.Log("Webserver request complete");

                string phrase = www.downloadHandler.text;

                retinaImage = JsonUtility.FromJson<RetinaImage>(phrase);

                Debug.Log("Retina image object updated.");

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
        byte[] imageBytes = Convert.FromBase64String(retinaImage.image);
        Texture2D texture = new Texture2D(retinaImage.xSize, retinaImage.ySize);
        yield return texture.LoadImage(imageBytes);

        GameObject newButton = Instantiate(button) as GameObject;
        newButton.transform.SetParent(buttonholder.transform, false);
        newButton.GetComponent<RawImage>().texture = texture;
        newButton.GetComponent<changeImage>().texture = texture;
        newButton.GetComponent<changeImage>().labels = retinaImage.labels;
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
                retinaImage.image = tempRetinaImage.image;
                retinaImage.labels = tempRetinaImage.labels;
                f.Close();

                StartCoroutine(makeButton(retinaImage));
            }
        }
    }
}
