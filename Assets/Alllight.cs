using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Alllight : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Light2D>().intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
