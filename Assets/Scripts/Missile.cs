using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool _launching = true;
    private Rigidbody2D _rigidbody;
    private readonly System.Random _random = new System.Random();

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<AudioSource>().pitch = 1.0f + (_random.Next(-2, 2) * 0.1f);
    }

    private void FixedUpdate()
    {
        if (!_launching) return;

        _launching = false;
        _rigidbody.AddForce(new Vector2(0, 200));
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }
}