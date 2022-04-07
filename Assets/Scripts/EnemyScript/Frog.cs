using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private int health = 2;
    private float frogSpeed = 4;
    public ParticleSystem frogEffect;
    
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
        transform.Translate(Vector2.right * Time.deltaTime * frogSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            health--;
            Destroy(collision.gameObject);
            if (health == 0)
            {
                frogEffect.Play();
                Destroy(gameObject);
                
                
            }
        }
        if (collision.gameObject.CompareTag("distance"))
        {
            
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (collision.gameObject.CompareTag("distance1"))
        {
            
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);//05335623482
        }
    }
}
