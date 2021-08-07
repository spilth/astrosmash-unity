using UnityEngine;
using Random = System.Random;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRate = 5.0f;

    private readonly Random _random = new Random();
    private float _elapsedTime;
    private Vector3 _transformPosition;
    private float _localScaleX;

    private void Start()
    {
        var transform1 = transform;
        _transformPosition = transform1.position;
        _localScaleX = transform1.localScale.x / 2;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > spawnRate)
        {
            _elapsedTime = 0;

            var xOffset = _random.Next(-(int) _localScaleX, (int) _localScaleX);

            var spawnPoint = new Vector3(_transformPosition.x + xOffset, _transformPosition.y, _transformPosition.z);
            Instantiate(meteorPrefab, spawnPoint, Quaternion.identity);
        }
    }
}