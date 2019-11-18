
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReaderScript : MonoBehaviour
{
    //public property to define this in each scene (no new script for each card needed)
    //default: SampleScene
    public string NextScene = SceneLoader.Scene.SampleScene.ToString();

    //Todo: add BoxCollider with "Is Trigger" activated
    private void OnTriggerEnter(Collision other)
    {
        //Load new Scene when in contact with CardReader
        if (other.gameObject.tag == "KeyCard")
        {
            //destroy Keycard
            Destroy(other.gameObject);

            //get elevatordoor of current scene and close it
            var elevatorDoor = GameObject.FindGameObjectWithTag("ElevatorDoor");
            elevatorDoor.SendMessage("CloseElevatorDoor");              //CloseElevatorDoor methode must be defined in ElevatorScript

            //Todo: simulate movement

            //load next scene
            SceneLoader.Load(this.NextScene);
        }
    }
}
