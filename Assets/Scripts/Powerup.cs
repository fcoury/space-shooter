﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int powerupID;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.ActivateTripleshot();
                        break;

                    case 1:
                        player.ActivateSpeedup();
                        break;

                    case 2:
                        // player.ActivateShield();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
