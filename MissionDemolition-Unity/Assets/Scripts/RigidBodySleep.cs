/****
* Created by; Rohit Khanolkar
* Date Created: 2/16/2022
* 
* Last Edited by: NA
* Last edited: 2/16/2022
* 
* 
* Description: Put the rigidbody to sleep

****/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class RigidBodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null) rb.Sleep();
   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
