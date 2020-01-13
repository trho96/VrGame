using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    int startCounter = 0;
    bool isFlickering = false;
    float maxIntensity;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        if (light == null)
            light = GetComponent<Light>();
        maxIntensity = light.intensity;

        isFlickering = true;

    }

    // Update is called once per frame
    void Update()
    {
      

        if (isFlickering)
        {
            startCounter++;
            if (startCounter == 100)
            {
                isFlickering = false;
            }
            if (Random.Range(0, 10) < 1)
            {
                light.intensity = Random.Range(0, maxIntensity);


            }
        }
    }

    void Flicker()
    {
        isFlickering = true;
    }
}
