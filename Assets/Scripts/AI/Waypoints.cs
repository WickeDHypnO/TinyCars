using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public List<Transform> points;

    private void OnDrawGizmos()
    {
        if(points.Count > 0)
        {
            Vector3 lastPosition = points[0].position;
            foreach(Transform t in points)
            {
                Gizmos.DrawSphere(t.position, 1);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(t.position, lastPosition);
                lastPosition = t.position;
            }
            Gizmos.DrawLine(lastPosition, points[0].position);
        }
    }
}
