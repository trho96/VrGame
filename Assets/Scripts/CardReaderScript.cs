
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReaderScript : MonoBehaviour
{
    //public property to define this in each scene (no new script for each card needed)
    //default: SampleScene
    public string NextScene = SceneLoader.Scene.SampleScene.ToString();
    private AsyncOperation async;
    //Todo: add BoxCollider with "Is Trigger" activated
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        //Load new Scene when in contact with CardReader
        if (other.CompareTag("KeyCard"))
        {
            //StartLoading();
            Debug.Log("Loading new scene");
            //destroy Keycard
            Destroy(other.gameObject);

            //get elevatordoor of current scene and close it
            var elevatorDoor = GameObject.FindGameObjectWithTag("ElevatorDoor");
            elevatorDoor.SendMessage("CloseElevatorDoor");              //CloseElevatorDoor methode must be defined in ElevatorScript

            //Todo: simulate movement
            Debug.Log("Starting teleport algorithm");
            StartCoroutine(Teleport());
            
        }
    }

    private IEnumerator Teleport()
    {
        Debug.Log("Waiting for teleport");
        //load next scene
        yield return new WaitForSeconds(3);
        Debug.Log("Teleporting");
        SceneLoader.Load(this.NextScene);
        //ActivateScene();
    }

    private void StartLoading()
    {
        StartCoroutine("loadScene");
    }

    IEnumerable loadScene()
    {
        async = Application.LoadLevelAsync(this.NextScene);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }
}
