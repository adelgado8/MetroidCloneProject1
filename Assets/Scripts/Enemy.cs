/*
 * Villalobos, Cameron
 * 4/18/24
 * This script keeps track of various variables related to the basic enemy, including speed, health, and damage.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public PlayerControl PlayerControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            health = (health - 1);
            EnemyLoseLife();
        }
    }


    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed = 3;
    public bool goingLeft;
    public int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    //When the enemy reaches one of the left or right positions, it turns around and moves until it reaches the other one.
    private void Move()
    {
        if (goingLeft)
        {
            if (transform.position.x <= leftPos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.x >= rightPos.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }

    //If the enemy runs out of health, it is destroyed.
   
    public void Damage()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
  
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void EnemyLoseLife()
    {
        GetComponent<Transform>().position = Vector3.zero;

        if (health == 0)
        {
        
            health = 0;
            SceneManager.LoadScene(2);
        }


    }
}
