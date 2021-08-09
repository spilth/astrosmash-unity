using UnityEngine;
using Random = System.Random;

public class AmmoCrateSpawner : MonoBehaviour
{
    public GameObject ammoCratePrefab;
    public float spawnRate = 20.0f;

    private readonly Random _random = new Random();
    private float _elapsedTime;
    private Vector3 _transformPosition;
    private float _localScaleX;
    private AudioSource _audioSource;

    private void Start()
    {
        var transform1 = transform;
        _transformPosition = transform1.position;
        _localScaleX = transform1.localScale.x / 2;
        
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > spawnRate)
        {
            _elapsedTime = 0;

            var xOffset = _random.Next(-(int) _localScaleX, (int) _localScaleX);

            var spawnPoint = new Vector3(_transformPosition.x + xOffset, _transformPosition.y, _transformPosition.z);
            var ammoCrate = Instantiate(ammoCratePrefab, spawnPoint, Quaternion.identity);
            ammoCrate.GetComponent<AmmoCrate>().SetSpawner(this);
        }
    }

    public void PlayPickupSound()
    {
        _audioSource.Play();
    }
}
