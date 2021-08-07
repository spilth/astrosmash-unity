using UnityEngine;

public class Explosion : MonoBehaviour
{
    private readonly System.Random _random = new System.Random();

    void Start()
    {
        GetComponent<AudioSource>().pitch = 1.0f + (_random.Next(-2, 2) * 0.1f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        Destroy(gameObject, 1f);
    }
}