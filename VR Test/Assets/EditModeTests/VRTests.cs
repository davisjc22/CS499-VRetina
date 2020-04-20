using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Tests
{
    public class VRTests
    {
        [Test]
        public void testZoomOptions_ZoomIn()
        {
            var gameObject = new GameObject();
            ZoomOptions zoomOptions = gameObject.AddComponent<ZoomOptions>() as ZoomOptions;
            GameObject EyeBall = GameObject.Find("Eyeball");
            var x = EyeBall.transform.localScale.x;
            var y = EyeBall.transform.localScale.y;
            var z = EyeBall.transform.localScale.z;
            var expectedX = x + 1;
            var expectedY = y + 1;
            var expectedZ = z + 1;
            var expectedScale = (expectedX + ", " + expectedY + ", " + expectedZ);

            zoomOptions.ZoomIn();
            var acttualPos = (EyeBall.transform.localScale.x + ", " + EyeBall.transform.localScale.y + ", " + EyeBall.transform.localScale.z);

            Assert.AreEqual(expectedScale, acttualPos);
        }


        [Test]
        public void testZoomOPtions_ZoomOut()
        {
            var gameObject = new GameObject();
            ZoomOptions zoomOptions = gameObject.AddComponent<ZoomOptions>() as ZoomOptions;
            GameObject EyeBall = GameObject.Find("Eyeball");
            var x = EyeBall.transform.localScale.x;
            var y = EyeBall.transform.localScale.y;
            var z = EyeBall.transform.localScale.z;
            var expectedX = x - 1;
            var expectedY = y - 1;
            var expectedZ = z - 1;
            var expectedScale = (expectedX + ", " + expectedY + ", " + expectedZ);

            zoomOptions.ZoomOut();
            var acttualPos = (EyeBall.transform.localScale.x + ", " + EyeBall.transform.localScale.y + ", " + EyeBall.transform.localScale.z);

            Assert.AreEqual(expectedScale, acttualPos);
        }


        [Test]
        public void testRetinaImage_RetinaImage()
        {
            // What the variable values should be when creating a new retina image
            var id = -1;
            var name = "";
            var xSize = 0;
            var ySize = 0;
            var filetype = "";
            var official = false;
            var uploaded = "";
            var image = "";

            RetinaImage retinaImage = new RetinaImage();

            Assert.AreEqual(id, retinaImage.id);
            Assert.AreEqual(name, retinaImage.name);
            Assert.AreEqual(xSize, retinaImage.xSize);
            Assert.AreEqual(ySize, retinaImage.ySize);
            Assert.AreEqual(filetype, retinaImage.filetype);
            Assert.AreEqual(official, retinaImage.official);
            Assert.AreEqual(uploaded, retinaImage.uploaded);            
            Assert.AreEqual(image, retinaImage.image);
        }


        [UnityTest]
        public void testReturnToMainMenuButton_LoadMainMenu()
        {
            var gameObject = new GameObject();
            ReturnToMainMenuButton returnToMainMenuButton = gameObject.AddComponent<ReturnToMainMenuButton>() as ReturnToMainMenuButton;
            var expectedSceneName = "Start";

            returnToMainMenuButton.LoadMainMenu();
            var actualSceneName = EditorSceneManager.GetActiveScene().name;

            Assert.AreEqual(expectedSceneName, actualSceneName);
        }



        [UnityTest]
        public void testVRButton()
        {
            var gameObject = new GameObject();
            MainMenu mainMenu = gameObject.AddComponent<MainMenu>() as MainMenu;
            var expectedSceneName = "VRDemo";

            mainMenu.LoadVR();
            var actualSceneName = EditorSceneManager.GetActiveScene().name;

            Assert.AreEqual(expectedSceneName, actualSceneName);
        }


        [UnityTest]
        public void testARButton()
        {
            var gameObject = new GameObject();
            MainMenu mainMenu = gameObject.AddComponent<MainMenu>() as MainMenu;
            var expectedSceneName = "ARDemo";
            
            mainMenu.LoadAR();
            var actualSceneName = EditorSceneManager.GetActiveScene().name;

            Assert.AreEqual(expectedSceneName, actualSceneName);
        }


        [Test]
        public void testReturnToSceneButton()
        {

        }


        [Test]
        public void testSceneMenuButton()
        {

        }


        [Test]
        public void testCursor()
        {
            
        }


        [Test]
        public void testGallery()
        {

        }

    }
}
