using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject VrCam;
    public GameObject VrObj;
    public GameObject FpCam;
    public int CamMode;
    private const int _numberCamModes = 2;

    void Start()
    {
        VrCam.SetActive(false);
        VrObj.SetActive(false);
        FpCam.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            CamMode = (CamMode++) %_numberCamModes;
            StartCoroutine(CamChange());
        }
        
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        switch (CamMode)
        {
            case 0:
                VrCam.SetActive(true);
                VrObj.SetActive(true);
                FpCam.SetActive(false);
                break;
            case 1:
                FpCam.SetActive(true);
                VrObj.SetActive(false);
                VrCam.SetActive(false);
                break;
        };
    }
}
