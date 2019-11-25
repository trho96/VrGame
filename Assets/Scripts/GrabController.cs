using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Valve.VR;

//These make sure that we have the components that we need
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(FixedJoint))]
public class GrabController : MonoBehaviour
{
    public SteamVR_Input_Sources Hand;//these allow us to set our input source and action.
    public SteamVR_Action_Boolean ToggleGripButton;

    private GameObject ConnectedObject;//our current connected object
    public List<GameObject> NearObjects = new List<GameObject>();//all objects that we could pick up
    private void Update()
    {
        //Debug.Log("pressed " + ToggleGripButton.GetStateDown(Hand));
        //Debug.Log("Test");
        if (ConnectedObject != null)//if we are holding somthing
        {
            Debug.Log(GameObject.Find("Cube").transform.localPosition);
            Debug.Log(GameObject.Find("Cube").transform.position);

            /* if (ConnectedObject.GetComponent<Interactable>().touchCount == 0)//if our object isn't touching anything
            {
                //first, we disconnect our object
                GetComponent<ConfigurableJoint>().connectedBody = null;
                GetComponent<FixedJoint>().connectedBody = null;

                //now we step our object slightly towards the position of our controller, this is because the fixed joint just freezes the object in whatever position it currently is in relation to the controller so we need to move it to the position we want it to be in. We could just set position to the position of the controller and be done with it but I like the look of it swinging into position.
                ConnectedObject.transform.position = Vector3.MoveTowards(ConnectedObject.transform.position, transform.position, .25f);
                ConnectedObject.transform.rotation = Quaternion.RotateTowards(ConnectedObject.transform.rotation, transform.rotation, 10);

                //reconnect the body to the fixed joint
                GetComponent<FixedJoint>().connectedBody = ConnectedObject.GetComponent<Rigidbody>();
            }
            else */
            if (ConnectedObject.transform.parent == null)//if it is touching something 
            {
                //switch from fixed joint to configurable joint
                //GetComponent<FixedJoint>().connectedBody = null;
                //GetComponent<ConfigurableJoint>().connectedBody = ConnectedObject.GetComponent<Rigidbody>();
                
            }
            if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
            {
                Release();
            }
        }
        else//if we aren't holding something
        {
            Debug.Log("No holding");
            if (ToggleGripButton.GetStateDown(Hand))//cheack if we want to pick somthing up
            {
                Debug.Log("grip");
                Grip();
            }
        }
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
        ConnectedObject.GetComponent<Rigidbody>().isKinematic = true;   
    }
    private void Release()
    {
        //disconnect all joints and set the connected object to null
        //GetComponent<ConfigurableJoint>().connectedBody = null;
        //GetComponent<FixedJoint>().connectedBody = null;
        ConnectedObject.GetComponent<Rigidbody>().isKinematic = false;
        ConnectedObject.transform.SetParent(null, true);
        
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