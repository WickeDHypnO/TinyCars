using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPosition : MonoBehaviour {

    public int position;
    public int currentWaypoint;
    public int currentLap = 1;
    public int numberOfWaypoints;
    public Vector3 lastWaypointPosition;
    public Vector3 nextWaypointDirection;
    public float distance;
    int test = 0;
    public bool player;

    void OnTriggerEnter(Collider col)
    {
        if (int.TryParse(col.name, out test))
        {
            int waypoint = int.Parse(col.name);
            if (waypoint == currentWaypoint + 1)
            {
                currentWaypoint = waypoint;
                lastWaypointPosition = col.transform.position;
                if (currentWaypoint == numberOfWaypoints)
                {
                    currentLap++;
                    if(player)
                    {
                        if(currentLap <= FindObjectOfType<RaceManager>().raceLaps)
                        FindObjectOfType<RaceUI>().SetLap(currentLap);
                        FindObjectOfType<RaceUI>().SetLapTime(FindObjectOfType<RaceManager>().raceTimer);
                    }
                    if(currentLap > FindObjectOfType<RaceManager>().raceLaps)
                    {
                        FindObjectOfType<RaceManager>().EndRace(this);
                    }
                    currentWaypoint = 0;
                }
                nextWaypointDirection = -col.transform.right;
            }
        }
    }

	void Start () {
        //numberOfWaypoints = FindObjectOfType<Waypoints>().points.Count;
        //lastWaypointPosition = FindObjectOfType<Waypoints>().points[0].position;
    }

	public float GetDistance () {
        distance = (transform.position - lastWaypointPosition).magnitude + currentWaypoint * 100 + currentLap * 10000;
        return distance;
    }
}
