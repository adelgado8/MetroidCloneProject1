/*
 * Villalobos, Cameron
 * 4/18/24
 * This script keeps track of various variables related to the player, such as their health, speed, and jump height. It also establishes the controls and checks for positioning.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rigidBody;
    public int jumpForce = 6;
    public bool isGrounded;
    public int health = 99;
    public bool healthUp = false;
    private bool facingLeft;

    public float Enemy = 0f;

    //This is where the player respawns to.
    private Vector3 startPosition;
    internal static object position;

    public GameObject laserPrefab;
    public bool shootLeft;
    public Laser bulletDMG;

    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player presses A, face and move to the left.
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if(!facingLeft)
            {
                transform.Rotate(0, 180, 0);
                facingLeft = true;
            }
        }

        //If the player presses D, face and move to the right.
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (facingLeft)
            {
                transform.Rotate(0, 180, 0);
                facingLeft = false;
            }
        }

        //If the player presses SPACE, shoot a laser.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        /*
        //If the player loses all of their health, respawn.
        if (health <= 0)
        {
            
            Respawn();
           
        }
        */
        //If the player loses all of their health.
        if (health <= 0)
        {
            LoseLife();
        }
        //If player kills all Enemies, the player will win.
        
        //Check for player's maximum health and the variables associated with allowing the player to jump.
        HealthCheck();
        spaceJump();
    }

    //Spawn a laser that shoots in the direction the player is facing.
    private void Shoot()
    {
        GameObject newLaser = Instantiate(laserPrefab, transform.position, transform.rotation, null);
        newLaser.GetComponent<Laser>().goingLeft = shootLeft;
    }

    //Take 15 damage when colliding with a regular enemy.
    //Take 35 damage when colliding with a hard enemy.
    //Gain 50 health when colliding with a health pack.
    //Gain 100 max health when colliding with an extra health pack.
    //Gain an increased jump height when colliding with a jump boost.
    //Gain an increased laser damage when colliding with a laser upgrade. (Not working)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reg Enemy")
        {
            health = (health - 15);
        }

        if (other.gameObject.tag == "Hard Enemy")
        {
            health = (health - 35);
        }

        if (other.gameObject.tag == "Health")
        {
            health = health + 50;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ExtraHealth")
        {
            health = 199;
            healthUp = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Jump")
        {
            jumpForce = 9;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Heavy")
        {
            //bulletDMG.damage = 3; < This code crashes the game.
            Destroy(other.gameObject);
        }

        //If we collide with portal trigger, teleport the player to the next area
        //and reset the start position
        if (other.gameObject.tag == "Portal")
        {
            //reset the startPos to the spawnPoint position
            startPosition = other.gameObject.GetComponent<Portal>().spawnPoint.transform.position;
            //bring the player back to the start position
            transform.position = startPosition;
        }
    }

    //Check whether there is ground below the player before allowing them to jump.
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

    //The player cannot have more than 99 health.
    //If the health upgrade is collected, the player cannot have more than 199 health.
    public void HealthCheck()
    {
        if (healthUp == false)
        {
            if (health > 99)
            {
                health = 99;
            }
        }
        else if (health > 199)
        {
            health = 199;
        }
    }

    //The player should blink and have a temporary invulnerability when damage is taken. (Not working)
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
    
    //The player respawns with full health at the start position.
    public void Respawn()
    {
        GetComponent<Transform>().position = startPosition;
        if (healthUp == false)
        {
            health = 99;
        }
        else
        {
            health = 199;
        }
    }
    
    public void LoseLife()
    {
        GetComponent<Transform>().position = Vector3.zero;
        
        if (healthUp == false)
        {
            health = 0;
        }
        else
        {
            health = 0;
            SceneManager.LoadScene(1);
        }


    }

   

}
