using System;
using System.Threading.Tasks;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _forceValue;

    [SerializeField]
    private float _parabolicTimeValue;
    public float TimeValue => _parabolicTimeValue;

    private Material _mat;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mat = GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        _rigidbody.velocity = transform.forward * _forceValue;
    }

    public void SetForceValue(float force)
    {
        _forceValue = force;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.TANK_TAG))
        {
            Debug.Log("hit another tank", other.collider.gameObject);
            TankDamage td = other.transform.GetComponent<TankDamage>();
            td.TryApplyDamage(50);
        }
        else
        {
            GameEvents.OnCurrentRoundCompleted?.Invoke();
        }
        _rigidbody.drag = 50f;
        HideBullet();
    }

    private async Task DissappearBullet()
    {
        Color color = _mat.color;
        while (_mat.color.a > 0.01f)
        {
            color.a -= 0.1f;
            await Task.Delay(200);
            _mat.color = color;

        }
    }
    private async void HideBullet()
    {

        await DissappearBullet();
        DestroyImmediate(this.gameObject);
    }
}
