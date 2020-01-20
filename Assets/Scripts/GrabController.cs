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

    private Vector3 lastPosition;
    private Vector3 lastlastPosition;

    private int update;

    private void Update()
    {
        update++;
        if (update % 10 == 0)
        {
            lastlastPosition = lastPosition;
            lastPosition = this.transform.position;
        }
        

       
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
       // Debug.Log("grip " + nearObject.name);
        GameObject NewObject = nearObject;
        if (NewObject != null)
            ConnectedObject = NewObject;


        ConnectedObject.transform.SetParent(this.transform, true);


        if (ConnectedObject.CompareTag("GrabbableNoRotate"))
        {
            ConnectedObject.transform.rotation = new Quaternion(0,0,0,0);
        }
        // ConnectedObject.transform.localPosition = Vector3.zero;
        // ConnectedObject.transform.rotation = Quaternion.RotateTowards(ConnectedObject.transform.rotation, transform.rotation, 10);
        Destroy(ConnectedObject.GetComponent<Rigidbody>());
    }
    private void Release()
    {
        ConnectedObject.AddComponent(typeof(Rigidbody));
        ConnectedObject.transform.SetParent(null, true);

        Vector3 speed = this.lastPosition - this.lastlastPosition;
        ConnectedObject.GetComponent<Rigidbody>().AddForce(speed * 1250);
        Debug.Log(speed);
        ConnectedObject = null;

    }
    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Triggered Collider:" + other.gameObject.name);
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Grabbable") || other.CompareTag("KeyCard") || other.CompareTag("GrabbableNoRotate"))
        {
            nearObject = other.gameObject;
        
        }
        else if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.CompareTag("Grabbable"))
                nearObject = other.gameObject.transform.parent.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Removed Collider:" + other.gameObject.name);
        //remove grabbable objects going out of range from our list
        if (other.CompareTag("Grabbable") || other.CompareTag("KeyCard") || other.CompareTag("GrabbableNoRotate"))
        {
            nearObject = null;
        }
        else if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.CompareTag("Grabbable"))
                nearObject = null;
        }
    }

}