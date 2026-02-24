using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class SurfacePlacementManager : MonoBehaviour
{
    public GameObject dustbinPrefab;
    public GameObject fanPrefab;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool dustbinPlaced = false;
    private bool fanPlaced = false;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (!dustbinPlaced)
            {
                GameObject obj = Instantiate(dustbinPrefab, hitPose.position, hitPose.rotation);
                obj.AddComponent<ARAnchor>();   // Add anchor component directly
                dustbinPlaced = true;
            }
            else if (!fanPlaced)
            {
                GameObject obj = Instantiate(fanPrefab, hitPose.position, hitPose.rotation);
                obj.AddComponent<ARAnchor>();   // Add anchor component directly
                fanPlaced = true;
            }
        }
    }
}