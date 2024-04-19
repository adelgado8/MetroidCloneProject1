/*
 * Villalobos, Cameron
 * 4/18/24
 * This script keeps track of various variables related to the hard enemy, including speed, health, and damage.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class HardEnemy : MonoBehaviour
{
    public int speed = 3;
    public bool goingLeft;
    public int health = 10;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //If the enemy runs out of health, it is destroyed.
    public void Damage()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //If the player is to the right of the enemy, it moves to the right. If not, the enemy moves left, effectively following the player based on its position related to the enemy.
    private void Move()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}
