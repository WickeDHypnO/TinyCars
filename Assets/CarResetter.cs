using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarResetter : MonoBehaviour {

    public bool AI;
    Rigidbody rigid;
    CarPosition carPosition;
    public float graceTime = 2f;
    public float waypointChecker;
    int lastCheckpoint;
    float timer = 0f;

	void Start () {
        rigid = GetComponent<Rigidbody>();
        carPosition = GetComponent<CarPosition>();
	}
	
	void Update () {
        timer += Time.deltaTime;
        waypointChecker += Time.deltaTime;
		if(AI)
        {
            if(rigid.velocity.magnitude < 0.1f && timer >= graceTime)
            {
                if(FindObjectOfType<RaceManager>().reverse)
                {
                    transform.forward = -carPosition.nextWaypointDirection;
                }
                else
                {
                    transform.forward = carPosition.nextWaypointDirection;
                }
               // transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                transform.position = new Vector3(carPosition.lastWaypointPosition.x, transform.position.y + 1f, carPosition.lastWaypointPosition.z);
                rigid.velocity = Vector3.zero;
                timer = 0;
            }
            if(waypointChecker >= 3f)
            {
                if(lastCheckpoint == carPosition.currentWaypoint)
                {
                    if (FindObjectOfType<RaceManager>().reverse)
                    {
                        transform.forward = -carPosition.nextWaypointDirection;
                    }
                    else
                    {
                        transform.forward = carPosition.nextWaypointDirection;
                    }
                    // transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                    transform.position = new Vector3(carPosition.lastWaypointPosition.x, transform.position.y + 1f, carPosition.lastWaypointPosition.z);
                    rigid.velocity = Vector3.zero;
                    timer = 0;
                }
                lastCheckpoint = carPosition.currentWaypoint;
                waypointChecker = 0;
            }
        }
        else
        {
           if(Input.GetButtonDown("Reset") && timer >= graceTime)
            {
                if (FindObjectOfType<RaceManager>().reverse)
                {
                    transform.forward = -carPosition.nextWaypointDirection;
                }
                else
                {
                    transform.forward = carPosition.nextWaypointDirection;
                }
                // transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                transform.position = new Vector3(carPosition.lastWaypointPosition.x, transform.position.y + 1f, carPosition.lastWaypointPosition.z);
                rigid.velocity = Vector3.zero;
                timer = 0;
            }
        }
	}
}
