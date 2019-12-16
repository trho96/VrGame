using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.OpenElevatorDoor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (this.state)
        {
            case 1:
                if (this.transform.position.x < 0)
                    this.transform.position = this.transform.position + new Vector3(0.01f, 0, 0);
                break;
            case 2:
                if (this.transform.position.x >= -.8)
                    this.transform.position = this.transform.position + new Vector3(-0.01f, 0, 0);
                break;
        }
    }

    void CloseElevatorDoor()
    {
        this.state = 1;
    }



    void OpenElevatorDoor()
    {
        this.state = 2;
    }
}
