using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneIndex : MonoBehaviour
{
    // Use [SerializeField] to expose private variables in the Inspector
    [SerializeField] private int indexPlay;
    [SerializeField] private int indexStory;

    // Public method to change the scene, can be called from a UI button
    public void Play()
    {
        SceneManager.LoadScene(indexPlay);
    }

    public void Story()
    {
        SceneManager.LoadScene(indexStory);
    }
}