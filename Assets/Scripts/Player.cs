using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int speed = 5;
    public float fireRate = 2.0f;
    public GameObject missilePrefab;

    private Rigidbody2D _rig;
    private bool _movingLeft;
    private bool _movingRight;
    private bool _firing;
    private float _elapsedTime;
    private int _score;
    private int _lives = 3;
    private Text _scoreComponent;
    private Text _livesComponent;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _scoreComponent = GameObject.Find("Score").GetComponent<Text>();
        _scoreComponent.text = _score.ToString();
        _livesComponent = GameObject.Find("Lives").GetComponent<Text>();
        _livesComponent.text = _lives.ToString();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        var horizontal = Input.GetAxis("Horizontal");

        _movingLeft = false;
        _movingRight = false;

        if (horizontal < 0)
        {
            _movingLeft = true;
            _movingRight = false;
        }

        if (horizontal > 0)
        {
            _movingRight = true;
            _movingLeft = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _firing = true;
        }
        else
        {
            _firing = false;
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _scoreComponent.text = _score.ToString();
    }

    private void FixedUpdate()
    {
        if (_movingLeft)
        {
            _rig.AddForce(new Vector2(-speed, 0));
        }

        if (_movingRight)
        {
            _rig.AddForce(new Vector2(speed, 0));
        }

        if (_firing)
        {
            if (_elapsedTime > fireRate)
            {
                _elapsedTime = 0;
                var transformPosition = transform.position;
                Instantiate(missilePrefab, new Vector3(transformPosition.x, transformPosition.y + 1.1f, 0),
                    Quaternion.identity);
            }
        }
    }

    public void RemoveLife()
    {
        _lives--;
        _livesComponent.text = _lives.ToString();
    }
}