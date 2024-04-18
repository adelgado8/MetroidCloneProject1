using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rigidBody;
    public int jumpForce = 6;
    public bool isGrounded;
    public int health = 99;

    private Vector3 startPosition;
    internal static object position;

    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        spaceJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health = (health - 15);
        }

        if (other.gameObject.tag == "Hard Enemy")
        {
            health = (health - 35);
        }
    }

    public void spaceJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f))
        {
            isGrounded = true;
            Debug.Log("I am grounded");
        }
        else
        {
            isGrounded = false;
            Debug.Log("I am not grounded");
        }

        if (Input.GetKeyDown("w") && isGrounded == true)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Health()
    {

    }

    public IEnumerable Blink()
    {
        for (int index = 0; index < 30;  index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void Respawn()
    {
        GetComponent<Transform>().position = startPosition;
        StartCoroutine((string)Blink());
    }
}
