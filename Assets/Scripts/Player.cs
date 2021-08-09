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
    private int _lifeCount = 3;
    private int _missileCount = 10;

    private Text _scoreText;
    private Text _livesText;
    private Text _missilesText;
    private AudioSource _audioSource;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        _scoreText.text = _score.ToString();
        _livesText = GameObject.Find("Lives").GetComponent<Text>();
        _livesText.text = _lifeCount.ToString();
        _missilesText = GameObject.Find("Missiles").GetComponent<Text>();
        _missilesText.text = _missileCount.ToString();
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
        _scoreText.text = _score.ToString();
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

                if (_missileCount > 0)
                {
                    _missileCount--;
                    _missilesText.text = _missileCount.ToString();

                    var transformPosition = transform.position;
                    Instantiate(missilePrefab, new Vector3(transformPosition.x, transformPosition.y + 1.1f, 0),
                        Quaternion.identity);
                }
                else
                {
                    _audioSource.Play();
                }
            }
        }
    }

    public void RemoveLife()
    {
        _lifeCount--;
        _livesText.text = _lifeCount.ToString();
    }

    public void AddMissiles(int count)
    {
        _missileCount += count;
        _missilesText.text = _missileCount.ToString();
    }
}