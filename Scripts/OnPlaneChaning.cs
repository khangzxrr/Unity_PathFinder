using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class OnPlaneChaning : MonoBehaviour
{
    public GameObject m_InfinitePlanePrefab;
    public ARPlaneManager ARPlaneManager;

    private GameObject m_InfinitePlane;

    public void Awake()
    {
        ARPlaneManager.planesChanged += OnPlanesChanged;
    }
    private void OnPlanesChanged(ARPlanesChangedEventArgs evt)
    {
        List<ARPlane> addedPlanes = evt.added;
        if (addedPlanes.Count > 0 && m_InfinitePlane == null)
        {
            //last plane is nearest plane
            ARPlane plane = addedPlanes[addedPlanes.Count - 1];
            Vector3 position = plane.center;
            m_InfinitePlane = Instantiate(m_InfinitePlanePrefab, position, Quaternion.identity);
        }
    }
}
