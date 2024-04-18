using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rigidBody;
    public int jumpForce = 6;
    public bool isGrounded;
    public int health = 99;
    private bool facingLeft;

    private Vector3 startPosition;
    internal static object position;

    public GameObject laserPrefab;
    public float spawnRate = 2f;
    public bool shootLeft;

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
            if(!facingLeft)
            {
                transform.Rotate(0, 180, 0);
                facingLeft = true;
            }
        }

        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (facingLeft)
            {
                transform.Rotate(0, 180, 0);
                facingLeft = false;
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        spaceJump();
    }

    private void Shoot()
    {
        GameObject newLaser = Instantiate(laserPrefab, transform.position, transform.rotation, null);
        newLaser.GetComponent<Laser>().goingLeft = shootLeft;
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
