using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    public ARUXAnimationManager ARUXAnimationManager;
    public GameObject originPrefab;
    public GameObject pathPrefab;

    public GameObject placedOrigin
    {
        get;
        private set;
    }

    public List<GameObject> placedPaths
    {
        get;
        private set;
    }
    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    public void Start()
    {
        placedPaths = new List<GameObject>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touched!");

                var ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("InfinitePlane")))
                {
                    if(placedOrigin == null)
                    {
                        placedOrigin = Instantiate(originPrefab, hit.point, Quaternion.identity);

                    }
                    else
                    {
                        placedPaths.Add(Instantiate(pathPrefab, hit.point, Quaternion.identity));
                    }

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }


                /*
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.Planes))
                {
                    Pose hitPose = s_Hits[0].pose;

                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {
                        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                        
                        m_NumberOfPlacedObjects++;
                    }
                    else
                    {
                        spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    }
                    
                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
                */
            }
        }
    }
}
