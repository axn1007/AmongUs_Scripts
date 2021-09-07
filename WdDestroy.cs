using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WdDestroy : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        //Destroy(gameObject, 6);
    }
}
