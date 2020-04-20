using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Tests
{
    public class PlayModeTestScript
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayModeTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }


        [UnityTest]
        public void testReturnToMainMenuButton_LoadMainMenu()
        {
            GameObject gameObject = new GameObject();
            ReturnToMainMenuButton returnToMainMenuButton = gameObject.AddComponent<ReturnToMainMenuButton>() as ReturnToMainMenuButton;
            var expectedSceneName = "Start";

            returnToMainMenuButton.LoadMainMenu();
            var actualSceneName = EditorSceneManager.GetActiveScene().name;

            Assert.AreEqual(expectedSceneName, actualSceneName);
        }
    }
}
