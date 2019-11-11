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

    //call this to load new scene (at the moment sync -> lag/freeze)
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
