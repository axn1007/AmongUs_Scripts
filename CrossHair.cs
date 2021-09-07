using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }
}
