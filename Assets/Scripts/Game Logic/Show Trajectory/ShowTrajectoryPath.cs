using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShowTrajectoryPath : MonoBehaviour
{
    [SerializeField]
    private TrailSphere _trailObjectPrefab;
    [SerializeField]
    private float _dividingFactor;
    [SerializeField]
    private List<TrailSphere> _trailSphereList = new List<TrailSphere>();
    private int _noOfPoints;
    private bool _surfaceFound = false;




    private void OnEnable()
    {

        GameEvents.OnTankRotationChanged += ShowPath;
        GameEvents.OnShootButtonPressed += HidePath;
        GameEvents.OnSurfaceFound += SurfaceFound;
    }
    private void OnDisable()
    {
        GameEvents.OnTankRotationChanged -= ShowPath;
        GameEvents.OnShootButtonPressed -= HidePath;
        GameEvents.OnSurfaceFound -= SurfaceFound;
        _trailSphereList.Clear();
    }

    private int _surfaceIndex;
    private void SurfaceFound(TrailSphere trailSphere)
    {
        _surfaceFound = true;
        _surfaceIndex = _trailSphereList.FindIndex(x => x == trailSphere);



    }
    private void HidePath()
    {
        foreach (var item in _trailSphereList)
        {
            item.Disable();
        }
        _surfaceFound = false;
    }
    private void ShowPath(Transform initialPoint, float force, float time)
    {

        _surfaceFound = false;
        int index = 0;
        for (int i = 0; i < 70; i++)
        {

            SetTrailSphere(i, initialPoint, force, i * _dividingFactor);
            index = i;

        }
        for (int i = _surfaceIndex; i < _trailSphereList.Count; i++)
        {
            _trailSphereList[i].Disable();
        }

    }

    private async void SetTrailSphere(int i, Transform initialPoint, float force, float time)
    {
        if (i > 70)
            return;
        Vector3 newPosition = initialPoint.position + (initialPoint.forward * force * time) + (0.5f * Physics.gravity * time * time);
        if (_trailSphereList.Count > i)
        {
            _trailSphereList[i].Enable();
            _trailSphereList[i].transform.position = newPosition;
        }
        else
        {
            TrailSphere g = Instantiate(_trailObjectPrefab, transform.position, Quaternion.identity, transform);
            _noOfPoints++;
            g.transform.position = newPosition;
            _trailSphereList.Add(g);
        }
        await Task.Delay(100);


    }





}
