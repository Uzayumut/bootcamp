using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject fireSpawnPos;
    public GameObject fireSpawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
        Instantiate(fireSpawn,fireSpawnPos.transform.position,Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(fireSpawn.gameObject);
    }
}
