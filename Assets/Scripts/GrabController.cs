using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Valve.VR;

//These make sure that we have the components that we need
[RequireComponent(typeof(Rigidbody))]
public class GrabController : MonoBehaviour
{
    public SteamVR_Input_Sources Hand;//these allow us to set our input source and action.
    public SteamVR_Action_Boolean ToggleGripButton;

    private GameObject ConnectedObject;//our current connected object
    public List<GameObject> NearObjects = new List<GameObject>();//all objects that we could pick up

    private Vector3 positionHack;

    private void Update()
    {
        //Debug.Log("pressed " + ToggleGripButton.GetStateDown(Hand));
        //Debug.Log("Test");
        if (ConnectedObject != null)//if we are holding somthing
        {
            Debug.Log("Ctrl Pos: " + transform.localPosition);
            Debug.Log("Local Pos: " + GameObject.Find("Cube").transform.localPosition);
            Debug.Log("Pos: " + GameObject.Find("Cube").transform.position);
            var worldPosition = ConnectedObject.transform.position + ConnectedObject.transform.parent.transform.position;
                positionHack = worldPosition;
            Debug.Log("World Pos: " + worldPosition);
            Debug.Log("Field Pos: " + positionHack);

            
            if (ConnectedObject.transform.parent == null)//if it is touching something 
            {
                //switch from fixed joint to configurable joint
                //GetComponent<FixedJoint>().connectedBody = null;
                //GetComponent<ConfigurableJoint>().connectedBody = ConnectedObject.GetComponent<Rigidbody>();
                
            }
            if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
            {
                Release(worldPosition);

            }
        }
        else//if we aren't holding something
        {
            if (ToggleGripButton.GetStateDown(Hand))//cheack if we want to pick somthing up
            {
                Debug.Log("grip");
                Grip();
            }
        }
    }

    public void LateUpdate()
    {
        Debug.Log("LATE Local Pos: " + GameObject.Find("Cube").transform.localPosition);
        Debug.Log("LATE Pos: " + GameObject.Find("Cube").transform.position);
        Debug.Log("LATE CTRL Local Pos: " + GameObject.Find("Controller (right)").transform.localPosition);
        Debug.Log("LATE CTRL Pos: " + GameObject.Find("Controller (right)").transform.position);
    }

    private void Grip()
    {
        if (NearObjects.Count == 0)
            return;
        GameObject NewObject = NearObjects[0];
        if (NewObject != null)
            ConnectedObject = NewObject;//find the Closest Grabbable and set it to the connected objectif it isn't null

        ConnectedObject.transform.SetParent(this.transform, false);
        ConnectedObject.transform.localPosition = Vector3.zero;
        ConnectedObject.transform.rotation = Quaternion.RotateTowards(ConnectedObject.transform.rotation, transform.rotation, 10);
        //ConnectedObject.GetComponent<Rigidbody>().isKinematic = true;   

        ConnectedObject.GetComponent<Rigidbody>().detectCollisions = false;
        ConnectedObject.GetComponent<Rigidbody>().useGravity = false;
        //        Destroy(ConnectedObject.GetComponent<Rigidbody>());
    }
    private void Release(Vector3 worldPos)
    {
        //disconnect all joints and set the connected object to null
        //GetComponent<ConfigurableJoint>().connectedBody = null;
        ConnectedObject.GetComponent<Rigidbody>().isKinematic = false;
        ConnectedObject.transform.SetParent(null, true);

        ConnectedObject.GetComponent<Rigidbody>().detectCollisions = true;
        ConnectedObject.GetComponent<Rigidbody>().useGravity = true;
        //ConnectedObject.transform.position = positionHack;

        //ConnectedObject.AddComponent<Rigidbody>();
        //Debug.Log(transform.localPosition);
        //ConnectedObject.GetComponent<Rigidbody>().position = this.transform.position;

        ConnectedObject = null;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Collider:" + other.gameObject.name);
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Grabbable"))
        {
            NearObjects.Add(other.gameObject);
        }
        Debug.Log(NearObjects);
    }
    void OnTriggerExit(Collider other)
    {
        //remove grabbable objects going out of range from our list
        if (other.CompareTag("Grabbable"))
        {
            NearObjects.Remove(other.gameObject);
        }
    }
    GameObject ClosestGrabbable()
    {
        //find the object in our list of grabbable that is closest and return it.
        GameObject ClosestGameObj = null;
        float Distance = float.MaxValue;
        if (NearObjects != null)
        {
            foreach (GameObject GameObj in NearObjects)
            {
                if ((GameObj.transform.position - transform.position).sqrMagnitude < Distance)
                {
                    ClosestGameObj = GameObj;
                    Distance = (GameObj.transform.position - transform.position).sqrMagnitude;
                }
            }
        }
        return ClosestGameObj;
    }
}