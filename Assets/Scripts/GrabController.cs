using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Valve.VR;

//These make sure that we have the components that we need
//[RequireComponent(typeof(Rigidbody))]
public class GrabController : MonoBehaviour
{
    public SteamVR_Input_Sources Hand;//these allow us to set our input source and action.
    public SteamVR_Action_Boolean ToggleGripButton;

    private GameObject ConnectedObject;//our current connected object
                                       // public HashSet<GameObject> NearObjects = new HashSet<GameObject>();//all objects that we could pick up
    private GameObject nearObject;

    private Vector3 positionHack;

    private void Update()
    {
       
        if (ConnectedObject != null)//if we are holding somthing
        {            
            if (ConnectedObject.transform.parent == null)//if it is touching something 
            {
              
            }
            if (ToggleGripButton.GetStateDown(Hand))// Check if we want to drop the object
            {
                Release();

            }
        }
        else//if we aren't holding something
        {
            if (ToggleGripButton.GetStateDown(Hand))//cheack if we want to pick somthing up
            {
            
                Grip();
            }
        }
    }

    public void LateUpdate()
    {
     
    }

    private void Grip()
    {
        if (nearObject == null)
            return;
        Debug.Log("grip " + nearObject.name);
        GameObject NewObject = nearObject;
        if (NewObject != null)
            ConnectedObject = NewObject;

        ConnectedObject.transform.SetParent(this.transform, true);
       // ConnectedObject.transform.localPosition = Vector3.zero;
       // ConnectedObject.transform.rotation = Quaternion.RotateTowards(ConnectedObject.transform.rotation, transform.rotation, 10);
        Destroy(ConnectedObject.GetComponent<Rigidbody>());
    }
    private void Release()
    {
        ConnectedObject.AddComponent(typeof(Rigidbody));
        ConnectedObject.transform.SetParent(null, true);
        ConnectedObject = null;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered Collider:" + other.gameObject.name);
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Grabbable"))
        {
            nearObject = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Removed Collider:" + other.gameObject.name);
        //remove grabbable objects going out of range from our list
        if (other.CompareTag("Grabbable"))
        {
            nearObject = null;
        }
    }

}