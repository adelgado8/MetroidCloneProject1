using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}