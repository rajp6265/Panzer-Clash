using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlacementManager : MonoBehaviour
{

    [SerializeField] private GameObject _crosshair;
    [SerializeField]
    private ARRaycastManager _aRRaycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
        _crosshair = this.transform.GetChild(0).gameObject;
        _crosshair.SetActive(false);
    }

    bool GetTouchPositionData(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private void Update()
    {

        _aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            if (!_crosshair.activeInHierarchy)
            {
                _crosshair.SetActive(true);
            }
        }
    }





}
