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
        Lean.Touch.LeanTouch.OnFingerTap += OnTap;
    }

    void OnTap(Lean.Touch.LeanFinger leanFinger)
    {
        if (leanFinger.ScreenPosition.IsPointOverUIObject())
        {
            Debug.Log("Over UI. skip this touch");
            return;
        }

        Debug.Log("Touched!");

        var ray = Camera.main.ScreenPointToRay(leanFinger.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("InfinitePlane")))
        {
            if (placedOrigin == null)
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
    }
    void Update()
    {
        
    }
}
