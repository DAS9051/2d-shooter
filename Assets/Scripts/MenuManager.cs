using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Method to change the scene
    // This method takes a string parameter 'name' which represents the name of the scene to be loaded
    public void ChangeScene(string name)
    {
        // Load the scene with the specified name
        // This method uses the SceneManager class from the UnityEngine.SceneManagement namespace
        // It loads a new scene based on the name provided, replacing the current scene
        SceneManager.LoadScene(name);
    }
}
