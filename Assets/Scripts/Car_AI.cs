using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car_AI : MonoBehaviour
{
    // Start is called before the first frame update
    public float safeDistance = 2f;
    public float carSpeed = 5f;
    public string[] tags;

    public GameObject currentTrafficRoute;
    public GameObject nextWaypoint;
    public int currentwapointNumber;

    private UnityEngine.AI.NavMeshAgent _carNavmesh;

    private void Start()
    {
        _carNavmesh = this.gameObject.GetComponent<NavMeshAgent>();
        _carNavmesh.speed=carSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, safeDistance);

        if (hit.transform)
        {
            for(int i=0;i<tags.Length;i++)
            {
                if (hit.transform.tag == tags[i])
                {
                    Stop();
                }
            }
        }
        else
        {
            Move();
        }
       

    }
    
    void Stop()
    {
        _carNavmesh.speed = 0;
    }
    void Move()
    {
        if(nextWaypoint==null)
        {
            _carNavmesh.speed = 0;
        }
        else
        {
            _carNavmesh.speed = carSpeed;
        }
        if (currentwapointNumber > 0)
        {
            if (_carNavmesh.speed == 0)
                _carNavmesh.speed = carSpeed;
            _carNavmesh.SetDestination(currentTrafficRoute.transform.GetChild(currentwapointNumber - 1).transform.position);
        }
        else
        {
            if (nextWaypoint != null)
            {
                if (_carNavmesh.speed == 0)
                    _carNavmesh.speed = carSpeed;
                _carNavmesh.SetDestination(nextWaypoint.transform.position);

            }
        }
        if(currentwapointNumber>0)
        {
            float distance = Vector3.Distance(transform.position,currentTrafficRoute.transform.GetChild(currentwapointNumber - 1).transform.position);
            if (distance <= 1)
                currentwapointNumber -= 1;
        }
        else
        {
            if(nextWaypoint!=null)
            {
                float distance = Vector3.Distance(transform.position, nextWaypoint.transform.position);
                if (distance <= 1)
                {
                    currentwapointNumber = 4;
                    currentTrafficRoute = nextWaypoint.transform.parent.gameObject;

                }
            }
            
        }
    }

}
