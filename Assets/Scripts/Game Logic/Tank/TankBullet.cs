using System;
using System.Threading.Tasks;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private ParticleSystem _blastParticle;
    [SerializeField]
    private float _parabolicTimeValue;

    private Material _mat;
    private Vector3 newPosition;
    private float _timer = 0;
    [SerializeField]
    private float _forceValue;
    private Vector3 currentVelocity = Vector3.zero;

    private TankData _tankData;
    private bool surfaceHit = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mat = GetComponent<MeshRenderer>().material;
        _rigidbody.isKinematic = true;
    }

    public void SetData(float force, float time, TankData tankData)
    {
        _rigidbody.isKinematic = false;
        _forceValue = force;
        _parabolicTimeValue = time;
        _tankData = tankData;
        // _rigidbody.velocity = CalculateVelocty(target, transform.position, time);

        _rigidbody.velocity = transform.forward * _forceValue;
        currentVelocity = transform.forward * _forceValue;
        // MoveBulletOnPath();

    }
    ContactPoint[] contactPoints;
    private void OnCollisionEnter(Collision other)
    {
        if (surfaceHit)
            return;

        Debug.Log("hit kb", other.collider.gameObject);
        _audioSource.Play();
        _blastParticle.Play();
        surfaceHit = true;
        if (other.collider.CompareTag(Constants.TANK_COLLISION_TAG))
        {
            contactPoints = new ContactPoint[other.contactCount];
            other.GetContacts(contactPoints);
            foreach (var item in contactPoints)
            {
                GameObject g = new GameObject();
                g.transform.position = item.point;
            }
            Debug.Log("hit another tank", other.collider.gameObject);
            TankDamage td = other.transform.GetComponent<TankDamage>();
            td.TryApplyDamage(20, () => GameEvents.OnWinnerFound?.Invoke(_tankData));
        }

        GameEvents.OnCurrentRoundCompleted?.Invoke();


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
        if (this != null)
            DestroyImmediate(this.gameObject);
    }
}
