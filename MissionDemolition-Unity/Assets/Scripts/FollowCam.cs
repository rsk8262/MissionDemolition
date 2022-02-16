/****
* Created by; Rohit Khanolkar
* Date Created: 2/14/2022
* 
* Last Edited by: NA
* Last edited: 2/14/2022
* 
* 
* Description: Camera to follow projectile

****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //****Variables****//
    static public GameObject POI; //Static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; //desired Z pos of the camera


    private void Awake()
    {
        camZ = this.transform.position.z;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (POI == null) return;

        //Vector3 destination = POI.transform.position;

        Vector3 destination;
        
        if (POI == null) //if no POI
        {
            destination = Vector3.zero; //destination is zero
        }
        else
        {
            destination = POI.transform.position;
            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null; //null the POI if the rigidbody is asleep
                    return; //in the next update
                }

            }

        }



        destination.x = Mathf.Max(minXY.x, destination.x); 
        destination.y = Mathf.Max(minXY.y, destination.y);

        //interpolate from current position to destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = camZ;

        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10; 

    }
}
