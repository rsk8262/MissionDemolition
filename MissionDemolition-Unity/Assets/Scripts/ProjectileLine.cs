/****
* Created by; Rohit Khanolkar
* Date Created: 2/16/2022
* 
* Last Edited by: NA
* Last edited: 2/16/2022
* 
* 
* Description: Create trailing line behind projectile
****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; //singleton

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this; //sets the singleton

        line = GetComponent<LineRenderer>(); //reference to lineRenderer
        line.enabled = false; //disable LineRenderer
        points = new List<Vector3>(); //new list

    }

    public GameObject poi
    {
        get { return (_poi); }
        set { _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoints();
            }   
        }
    }

    void AddPoints()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
