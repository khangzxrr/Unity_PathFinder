using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacementManager : MonoBehaviour
{
    public ARRaycastManager aRRaycastManager; //control ray to place object manager
    public GameObject originPrefab; //origin model
    public GameObject pointerPrefab; //pointer model

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended) //if finger is released
        {
            List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("InfinitePlane")))
            {
                
            }
            


        }

    }
}
