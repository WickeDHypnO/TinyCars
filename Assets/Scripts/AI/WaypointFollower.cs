using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    List<Transform> points;
    int currentPointIndex;
    CarMovement carControl;
    Vector3 currentPoint;
    Vector3 flattenedPosition;
    public float nextPointDistance;
    public float thrustMultiplier;
    public AnimationCurve turnValue;
    public float nextPointDistanceRandom;
    float defaultPointDistance;
    void Start()
    {
        points = FindObjectOfType<Waypoints>().points;
        carControl = GetComponent<CarMovement>();
        currentPoint = new Vector3(points[currentPointIndex].position.x, 0, points[currentPointIndex].position.z);
        defaultPointDistance = nextPointDistance;
    }


    void Update()
    {
        if(!carControl.canDrive)
        {
            return;
        }
        flattenedPosition = new Vector3(transform.position.x, 0, transform.position.z);
        if (Vector3.Distance(currentPoint, flattenedPosition) < nextPointDistance)
        {
            currentPointIndex++;
            if (currentPointIndex == points.Count)
                currentPointIndex = 0;
            currentPoint = new Vector3(points[currentPointIndex].position.x, 0, points[currentPointIndex].position.z);
            nextPointDistance = defaultPointDistance + Random.Range(-nextPointDistanceRandom, nextPointDistanceRandom);
        }
        Vector3 relativeWaypointPosition = transform.InverseTransformPoint(new Vector3(
                                                   currentPoint.x,
                                                   0,
                                                   currentPoint.z));


        // by dividing the horizontal position by the magnitude, we get a decimal percentage of the turn angle that we can use to drive the wheels
        carControl.Turn(turnValue.Evaluate(relativeWaypointPosition.x / relativeWaypointPosition.magnitude));

        // now we do the same for torque, but make sure that it doesn't apply any engine torque when going around a sharp turn...
        if (Mathf.Abs(turnValue.Evaluate(relativeWaypointPosition.x / relativeWaypointPosition.magnitude)) < 0.5)
        {
            carControl.Accelerate(relativeWaypointPosition.z / relativeWaypointPosition.magnitude * thrustMultiplier);
        }
        else
        {
            //if (rigid.velocity.magnitude > 1)
            //{
            carControl.Accelerate(0.8f * thrustMultiplier);
           // carControl.Accelerate(0.95f * thrustMultiplier);
            //}
        }
    }
}