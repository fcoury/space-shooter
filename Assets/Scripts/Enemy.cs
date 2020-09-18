using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    void Start()
    {
        Debug.Log("Spawned Enemy");
        float x = Random.Range(-8f, 8f);
        transform.position = new Vector3(x, 7f, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            float x = Random.Range(-8f, 8f);
            transform.position = new Vector3(x, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy collided with: " + other.tag);
        if (other.CompareTag("Player"))
        {
            _player.Damage();
            Destroy(this.gameObject);
        }
        
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            Destroy(this.gameObject);
        }
    }

}
