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

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }


    void AddPoints()
    {
        Vector3 pt = _poi.transform.position;

        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return;
        }
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;

        } else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }

    }

    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }


    }

    private void FixedUpdate()
    {
        if (poi == null)
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                } else
                {
                    return;
                }

            } else
            {
                return;
            }

        }

        AddPoints();

        if (FollowCam.POI == null)
        {
            poi = null;
        }

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
