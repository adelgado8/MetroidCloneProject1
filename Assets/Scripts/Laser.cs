using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 5;
    public bool goingLeft;
    public float despawnTime = 5f;
    public float stunTime = 2f;

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

    private IEnumerator DespawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
