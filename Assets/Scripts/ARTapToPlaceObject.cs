using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject gameObjectToInstancetiate;

    private GameObject spawndObject;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 touchPosition;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();



    // Start is called before the first frame update
    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
    }
    
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        if (_aRRaycastManager.Raycast(touchPosition, hits, trackableTypes: UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawndObject == null)
            {
                spawndObject = Instantiate(gameObjectToInstancetiate, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawndObject.transform.position = hitPose.position;
            }
        }

    }

}
