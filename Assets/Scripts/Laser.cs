/*
 * Villalobos, Cameron
 * 4/18/24
 * This script keeps track of various variables related to the bullets such as their damage, the time it takes for them to disappear, and the direction they're moving.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 15;
    public bool goingLeft;
    public float despawnTime = 5f;
    public float stunTime = 2f;
    public int damage = 1;
    public bool bulletsUp = false;

    //I attempted to reference code from the "Enemy" and "HardEnemy" scripts, but couldn't figure it out.
    public Enemy enemyHP;
    public HardEnemy hardEnemyHP;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnTimer(despawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    //Destroys bullets after enough time has passed.
    private IEnumerator DespawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    //I attempted to change the enemies health variables from this script, but it was unsuccessful.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyHP.health = --(damage);
        }

        if (other.gameObject.tag == "Hard Enemy")
        {
            hardEnemyHP.health = --(damage);
        }
    }
}
