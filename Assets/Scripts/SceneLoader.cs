using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    //define Scenes here
    public enum Scene
    {
        SampleScene,
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
