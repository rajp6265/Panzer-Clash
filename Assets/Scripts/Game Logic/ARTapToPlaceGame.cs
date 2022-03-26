using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceGame : MonoBehaviour
{
    [SerializeField] private GameSceneData _gameSceneDataPrefab;
    [SerializeField] private GameObject _crosshair;

    private GameSceneData _spawnedGameSceneData = null;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 _touchPosition;
    private bool planeFound = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();

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
        if (!GetTouchPositionData(out _touchPosition) && planeFound)
            return;

        if (_aRRaycastManager.Raycast(_touchPosition, hits, TrackableType.PlaneWithinPolygon) && _crosshair.activeInHierarchy)
        {
            _crosshair.SetActive(false);
            var hitPose = hits[0].pose;
            if (_spawnedGameSceneData == null)
            {
                _spawnedGameSceneData = Instantiate(_gameSceneDataPrefab, hitPose.position, hitPose.rotation);
                planeFound = true;
            }
            else
            {
                _spawnedGameSceneData.gameObject.transform.position = hitPose.position;
            }
        }
    }


}
