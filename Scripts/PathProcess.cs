using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class PathProcess : MonoBehaviour
{
    public PlaceObjectsOnPlane objectsOnPlane;

    public float angleBetween(Vector3 first, Vector3 second)
    {
        var cosineAngle = Vector3.Dot(first, second) / (Vector3.Magnitude(first) * Vector3.Magnitude(second));
        return Mathf.Acos(cosineAngle) * 57.2958f; //rad to degree turn angle
    }

    public bool determineRightOrLeft(Vector3 first, Vector3 second)
    {
        var direction = first.x * second.z -
                        first.z * second.x;

        return (direction <= 0) ? true : false; //true = turn right, false = turn left
    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName())
            .AddressList.First(
                f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            .ToString();
    }

    public PathInfo GetPathBetween(Vector3 first, Vector3 second)
    {
        first = first.normalized;
        second = second.normalized;
        return new PathInfo(angleBetween(first, second), determineRightOrLeft(first, second));
    }
    public void Run()
    {
        if (objectsOnPlane.placedOrigin == null) return; //if origin is not placed
        if (objectsOnPlane.placedPaths.Count == 0) return; //if there isn't any path node placed

        var origin = objectsOnPlane.placedOrigin;
        var paths = new List<GameObject>(objectsOnPlane.placedPaths); //clone placedPaths

        var newPosForwardOrigin = origin.transform.position + origin.transform.forward;

        var OriginDirection = newPosForwardOrigin - origin.transform.position;
        var OriginToFirstNodeDirection = paths[0].transform.position - origin.transform.position;

        var originToFirstPathInfo = GetPathBetween(OriginDirection, OriginToFirstNodeDirection);
        originToFirstPathInfo.distance = Vector3.Distance(paths[0].transform.position, origin.transform.position);
        List<PathInfo> pathInfos = new List<PathInfo>(); //list contain path information
        pathInfos.Add(originToFirstPathInfo);

        Debug.Log(string.Format("Turn {0} angle {1:0.00} distance: {2: 0.00}", (originToFirstPathInfo.rol) ? "right" : "left", originToFirstPathInfo.angle, originToFirstPathInfo.distance));

        //Debug.Log(GetLocalIPv4());

        if (paths.Count < 2) return;

        var firstPath = paths[0];
        var firstDirection = OriginToFirstNodeDirection;
        paths.RemoveAt(0);

        foreach(var secondPath in paths)
        {
            var secondDirection = (secondPath.transform.position - firstPath.transform.position);
            var currentPathInfo = GetPathBetween(firstDirection, secondDirection);
            currentPathInfo.distance = Vector3.Distance(secondPath.transform.position, firstPath.transform.position);
            pathInfos.Add(currentPathInfo);

            Debug.Log(string.Format("normal turn {0} angle {1:0.00} Distance: {2: 0.00}", (currentPathInfo.rol) ? "right" : "left", currentPathInfo.angle, currentPathInfo.distance));

            //update previous value 
            firstDirection = secondDirection;
            firstPath = secondPath;
        }
        

    }
}
