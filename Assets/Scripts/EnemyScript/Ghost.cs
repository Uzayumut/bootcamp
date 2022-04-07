using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private int ghostSpeed = 2;
    public Transform fireSpawn;
    private int ghostLife = 5;
    public GameObject fire;
    private Color fireColor;
    public int fireSpeed = 5;
    private Quaternion quad;
    void Start()
    {
        quad = Quaternion.Euler(0, 180, 0);
        fireColor =fire.GetComponent<SpriteRenderer>().color;
        fireColor = Color.blue;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime * ghostSpeed);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("distance"))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (collision.gameObject.CompareTag("distance1"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            ghostLife--;
            Destroy(collision.gameObject);
            if (ghostLife == 0)
            {
                
                Destroy(gameObject);


            }
        }
    }

    public void LeftFire()
    {
        fire.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        Instantiate(fire, fireSpawn.position, Quaternion.identity);
        fire.transform.Translate(Vector2.left * Time.deltaTime * fireSpeed);
    }
    public void RightFire()
    {
        fire.transform.rotation = Quaternion.Euler(0, 180, 0);
        
        Instantiate(fire, fireSpawn.position, Quaternion.Euler(0, 180, 0));
        fire.transform.Translate(Vector2.right * Time.deltaTime * fireSpeed);
    }
    public void RotateFire()
    {
        if (transform.rotation == quad)
        {
            LeftFire();
            Debug.Log("LeftFire");
        }
        else
        {
            RightFire();
            Debug.Log("RightFire");
        }
    }
}
