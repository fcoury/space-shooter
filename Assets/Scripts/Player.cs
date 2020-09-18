using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _speedupFactor = 4f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleshotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private bool _tripleshotActive = true;

    [SerializeField]
    private bool _speedupActive = true;

    private float _nextFire = 0.0f;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        if (_tripleshotActive)
        {
            Instantiate(_tripleshotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 1.05f, 0);
            Instantiate(_laserPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void ActivateTripleshot()
    {
        this._tripleshotActive = true;
        StartCoroutine(DrainTripleshotRoutine());
    }

    public void ActivateSpeedup()
    {
        this._speedupActive = true;
        StartCoroutine(DrainSpeedupRoutine());
    }

    public void DeactivateTripleshot()
    {
        this._tripleshotActive = false;
    }

    public void DeactivateSpeedup()
    {
        this._speedupActive = false;
    }

    IEnumerator DrainTripleshotRoutine()
    {
        yield return new WaitForSeconds(5f);
        DeactivateTripleshot();
    }

    IEnumerator DrainSpeedupRoutine()
    {
        yield return new WaitForSeconds(5f);
        DeactivateSpeedup();
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
            if (_spawnManager != null)
            {
                _spawnManager.OnPlayerDeath();
            }
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        float speed = _speed * (_speedupActive ? _speedupFactor : 1);
        transform.Translate(direction * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
}
