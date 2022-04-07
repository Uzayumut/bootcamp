using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float fireSpeed = 6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector3.right*Time.deltaTime*fireSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Tile"))
        {
            Destroy(gameObject);
        }
    }
}
