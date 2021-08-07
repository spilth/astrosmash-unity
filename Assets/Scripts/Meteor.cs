using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject explosionPrefab;

    private bool _launching = true;
    private Rigidbody2D _rigidbody;
    private readonly System.Random _random = new System.Random();
    private readonly int _sway = 40;
    private readonly int _spin = 20;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_launching) return;

        _launching = false;

        var leftRight = _random.Next(-_sway, _sway);
        _rigidbody.AddForce(new Vector2(leftRight, 0));
        _rigidbody.AddTorque(_random.Next(-_spin, _spin));
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        switch (collision2D.gameObject.name)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Ground":
                Explode();
                FindObjectOfType<Player>().AddScore(-10);
                break;
            case "Missile(Clone)":
                Explode();
                FindObjectOfType<Player>().AddScore(10);
                break;
            case "Player":
                Explode();
                FindObjectOfType<Player>().RemoveLife();
                break;
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}