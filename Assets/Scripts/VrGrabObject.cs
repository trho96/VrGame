using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteamVR.Scripts.SteamVR_Controller;

public class VrGrabObject : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller

    {

        get

        {

            return SteamVR_Controller.Input((int)trackedObj.index);

        }

    }

    void Awake()

    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    public GameObject collidingObject;

    public GameObject objectInHand;

    void OnTriggerEnter(Collider other)

    {

        if (!other.GetComponent<Rigidbody>())

        {

            return;

        }

        collidingObject = other.gameObject;

    }

    void OnTriggerExit(Collider other)

    {

        collidingObject = null;

    }

    void Update()

    {

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))

        {

            if (collidingObject)

            {

                GrabObject();

            }

        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))

        {

            if (objectInHand)

            {

                ReleaseObject();

            }

        }

    }

    private void GrabObject()

    {

        objectInHand = collidingObject;

        objectInHand.transform.SetParent(this.transform);

        objectInHand.GetComponent<Rigidbody>().isKinematic = true;

    }

    

    private void ReleaseObject()

    {

        objectInHand.GetComponent<Rigidbody>().isKinematic = false;

        objectInHand.transform.SetParent(null);

    }
}
