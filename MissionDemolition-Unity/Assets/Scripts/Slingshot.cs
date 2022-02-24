/****
* Created by; Rohit Khanolkar
* Date Created: 2/9/2022
* 
* Last Edited by: NA
* Last edited: 2/9/2022
* 
* 
* Description:

****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    /** Variables **/
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;


    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos; //launch position of projectile
    public GameObject projectile; //instance of projectile
    public bool aimingMode; //is player aiming
    public Rigidbody projectileRB; //RigidBody of projectile


    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;

            return S.launchPos;

        }

    }


    private void Awake()
    {
        S = this;

        Transform launchPointTrans = transform.Find("LaunchPoint"); //find child transform

        launchPoint = launchPointTrans.gameObject; //the game object of of the child object
        launchPoint.SetActive(false); //disable game object
        launchPos = launchPointTrans.position;
    }

    private void Update()
    {
        if (!aimingMode) return;

        //get the current mouse position in 2d screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; //pixel amount of change between the mouse3D and launchPos

        //limit mouseDelta to slingshot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize(); //sets the vector to the same direction, but length is 1
            mouseDelta *= maxMagnitude;
        }

        //move projectile to new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos; 

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;

            FollowCam.POI = projectile;//set the poi for the camera

            projectile = null; //forget last instance

            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }

    }



    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
        print("Slingshot: OnMouseEnter");
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false); 
        print("Slingshot: OnMouseExit");
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;

    }



}
